using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gameplay.Units;
using Gameplay.Units.Player;
using UnityEngine;

public class PlayerSubmarine : MonoBehaviour {
	public event Action OnDestroy;
	public event Action OnShot;
	public event Action<float> OnHealthUpdate;
	public event Action<RoomType> OnRoomDisabled;
	public event Action<RoomType> OnRoomEnabled;
	public event Action<RoomType, float> OnRoomHealthUpdate;
	public event Action<int> OnCrewMemberDeath;
	public event Action<int, float> OnCrewMemberHealthUpdate;

	[SerializeField] private Submarine Submarine;
	[SerializeField] private ControllerSubmarineHealthbar Healthbar;
	[SerializeField] private PlayerSubmarineConfig Config;
	[SerializeField] private PlayerSubmarineModel model;


	private Dictionary<RoomType, RoomConfigModel> roomsConfigsDictionary = new Dictionary<RoomType, RoomConfigModel>();
	private Dictionary<RoomType, RoomModel> roomsModelsDictionary = new Dictionary<RoomType, RoomModel>();
	private Dictionary<int, CrewMemberConfigModel> crewMembersConfig = new Dictionary<int, CrewMemberConfigModel>();

	private void Awake(){
		model = new PlayerSubmarineModel(){
			Health = Config.Model.MaxHealth,
			WeaponsReloadProgress = 1.0f,
			Rooms = Config.Model.Rooms.Select(room => new RoomItem(){
				Type = room.Type,
				Model = new RoomModel(){
					Health = room.Model.MaxHealth,
					IsActive = true,
					CrewModels = room.Model.CrewModels.Select(model => new CrewMemberModel(){
						Health = model.MaxHealth,
						Id = model.Id,
					}).ToList(),
				}
			}).ToList(),
		};

		{
			foreach (var roomItem in Config.Model.Rooms){
				roomsConfigsDictionary.Add(roomItem.Type, roomItem.Model);
			}

			foreach (var roomItem in model.Rooms){
				roomsModelsDictionary.Add(roomItem.Type, roomItem.Model);
			}

			var x = Config.Model.Rooms.SelectMany(item => item.Model.CrewModels);
			foreach (CrewMemberConfigModel configModel in x){
				crewMembersConfig.Add(configModel.Id, configModel);
			}
		}
	}

	public void SetFireEnabled(bool isEnabled) => model.IsNeedShot = isEnabled;

	public bool IsRoomEnabled(RoomType roomType) => roomsModelsDictionary[roomType].IsActive;

	public float GetMaxRoomHP(RoomType roomType) => roomsConfigsDictionary[roomType].MaxHealth;

	public float GetDamageValue() => Config.Model.RoomsConfig.GetWeaponsRoomValue(
		roomsModelsDictionary[RoomType.Weapons].CrewModels.Count);

	public float GetAccuracy() => Config.Model.RoomsConfig.GetSonarRoomValue(
		roomsModelsDictionary[RoomType.Sonar].CrewModels.Count);

	public float GetMobility() => Config.Model.RoomsConfig.GetEngineRoomValue(
		roomsModelsDictionary[RoomType.Engine].CrewModels.Count);

	private float GetMedBayHealValue() => Config.Model.RoomsConfig.GetMedBayRoomValue();

	private float GetHullRepairValue() => Config.Model.RoomsConfig.GetHullRepairRoomValue(
		roomsModelsDictionary[RoomType.HullRepair].CrewModels.Count);

	public float GetCrewMemberMaxHealthById(int id) => crewMembersConfig[id].MaxHealth;

	public Vector3 GetFirePoint() => Submarine.GetFirePoint();
	public Vector3 GetHitPoint() => Submarine.GetHitPoint();
	public Vector3 GetMissPoint() => Submarine.GetMissPoint();

	public void Update(){
		HealTick(Time.deltaTime);
		HullRepairTick(Time.deltaTime);
		RoomsRepairTick(Time.deltaTime);
		WeaponsTick(Time.deltaTime);
	}

	public void TakeHullDamage(float damage){
		model.Health = Mathf.Max(model.Health - damage, 0.0f);
		Healthbar.SetFill(model.Health/Config.Model.MaxHealth);
		OnHealthUpdate?.Invoke(model.Health);
		if (model.Health <= 0.0f){
			OnDestroy?.Invoke();
		}
	}

	public void TakeRoomDamage(RoomType roomType, float damage){
//		Debug.Log(roomType);
		var room = roomsModelsDictionary[roomType];
		float newHealth = Mathf.Max(room.Health - damage, 0.0f);
		room.Health = newHealth;
		OnRoomHealthUpdate?.Invoke(roomType, newHealth);

		if (room.IsActive && newHealth <= 0.0f){
			room.IsActive = false;
			OnRoomDisabled?.Invoke(roomType);
		}

		float damagePerCrewMember = damage;
		for (var index = room.CrewModels.Count - 1; index >= 0; index--){
			CrewMemberModel crewModel = room.CrewModels[index];
			crewModel.Health = Mathf.Max(crewModel.Health - damagePerCrewMember, 0.0f);
			OnCrewMemberHealthUpdate?.Invoke(crewModel.Id, crewModel.Health);
			if (crewModel.Health <= 0.0f){
				room.CrewModels.Remove(crewModel);
				crewMembersConfig.Remove(crewModel.Id);
				OnCrewMemberDeath?.Invoke(crewModel.Id);
			}
		}
	}

	private void HealTick(float deltaTime){
		if (!IsRoomEnabled(RoomType.MedBay)) return;

		float medBayHealValue = GetMedBayHealValue();
		foreach (CrewMemberModel crewModel in roomsModelsDictionary[RoomType.MedBay].CrewModels){
			float maxHealthOfCrewMember = crewMembersConfig[crewModel.Id].MaxHealth;
			if (crewModel.Health < maxHealthOfCrewMember){
				crewModel.Health = Mathf.Min(crewModel.Health + medBayHealValue * deltaTime, maxHealthOfCrewMember);
				OnCrewMemberHealthUpdate?.Invoke(crewModel.Id, crewModel.Health);
			}
		}
	}

	private void HullRepairTick(float deltaTime){
		if (!IsRoomEnabled(RoomType.HullRepair)) return;

		if (model.Health < Config.Model.MaxHealth){
			var repairValue = GetHullRepairValue();
			model.Health = Mathf.Min(model.Health + repairValue * deltaTime, Config.Model.MaxHealth);
			OnHealthUpdate?.Invoke(model.Health);
		}
	}

	private void RoomsRepairTick(float deltaTime){
		foreach (RoomItem roomItem in model.Rooms){
			var roomConfig = roomsConfigsDictionary[roomItem.Type];
			if (!roomItem.Model.IsActive && roomItem.Model.Health < roomConfig.MaxHealth){
				roomItem.Model.Health =
					Mathf.Min(roomItem.Model.Health + roomConfig.RepairingSpeedPerCrewMember * roomItem.Model.CrewModels.Count * deltaTime,
							  roomConfig.MaxHealth);
				OnRoomHealthUpdate?.Invoke(roomItem.Type, roomItem.Model.Health);

				if (!roomItem.Model.IsActive && roomItem.Model.Health == roomConfig.MaxHealth){
					roomItem.Model.IsActive = true;
					OnRoomEnabled?.Invoke(roomItem.Type);
				}
			}
		}
	}

	public void WeaponsTick(float deltaTime){
		if (!roomsModelsDictionary[RoomType.Weapons].IsActive) return;

		if (model.WeaponsReloadProgress < 1.0f){
			float appendReload = Config.Model.RoomsConfig.GetWeaponsRoomValue(roomsModelsDictionary[RoomType.Weapons].CrewModels.Count) * deltaTime;
			model.WeaponsReloadProgress = Mathf.Min(model.WeaponsReloadProgress + appendReload, 1.0f);
		}

		if (model.IsNeedShot && model.WeaponsReloadProgress == 1.0f){
			model.WeaponsReloadProgress = 0.0f;
			OnShot?.Invoke();
		}
	}

	public void MoveCrewMember(int crewMemberId, RoomType fromRoom, RoomType toRoom){
		int inRoomCrewMemberIndex = roomsModelsDictionary[fromRoom].CrewModels.FindIndex(memberModel => memberModel.Id == crewMemberId);

		if (inRoomCrewMemberIndex == -1){
			throw new ArgumentException($"Нельзя переместить чувака с id {crewMemberId} из отсека {fromRoom} - его там нет!");
		}

		if (fromRoom == toRoom){
			return;
		}

		roomsModelsDictionary[toRoom].CrewModels.Add(roomsModelsDictionary[fromRoom].CrewModels[inRoomCrewMemberIndex]);
		roomsModelsDictionary[fromRoom].CrewModels.RemoveAt(inRoomCrewMemberIndex);
	}

	#region Tests

	[ContextMenu(nameof(Test_GetHullDamage))]
	public void Test_GetHullDamage() => TakeHullDamage(100);

	[ContextMenu(nameof(Test_GetMedBayDamage))]
	public void Test_GetMedBayDamage() => TakeRoomDamage(RoomType.MedBay, 50);

	[ContextMenu(nameof(Test_GetEngineDamage))]
	public void Test_GetEngineDamage() => TakeRoomDamage(RoomType.Engine, 50);

	[ContextMenu(nameof(Test_MoveCrewMember1FromWeaponsToMedBay))]
	public void Test_MoveCrewMember1FromWeaponsToMedBay() => MoveCrewMember(1, RoomType.Weapons, RoomType.MedBay);

	[ContextMenu(nameof(Test_MoveCrewMember1FromMedBayToWeapons))]
	public void Test_MoveCrewMember1FromMedBayToWeapons() => MoveCrewMember(1, RoomType.MedBay, RoomType.Weapons);

	[ContextMenu(nameof(Test_ShowInfo))]
	public void Test_ShowInfo(){
		StringBuilder sb = new StringBuilder();
		sb.Append($"{nameof(GetDamageValue)} {GetDamageValue()}\n");
		sb.Append($"{nameof(GetAccuracy)} {GetAccuracy()}\n");
		sb.Append($"{nameof(GetMobility)} {GetMobility()}\n");
		sb.Append($"{nameof(GetMedBayHealValue)} {GetMedBayHealValue()}\n");
		sb.Append($"{nameof(GetHullRepairValue)} {GetHullRepairValue()}\n");
		Debug.Log(sb);
	}

	#endregion
}