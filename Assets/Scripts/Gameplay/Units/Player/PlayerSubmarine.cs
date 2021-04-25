using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Units;
using Gameplay.Units.Player;
using UnityEngine;

public class PlayerSubmarine : MonoBehaviour {
	public event Action OnDestroy;
	public event Action<RoomType> OnRoomRepaired;
	public event Action<int> OnCrewDeath;

	[SerializeField] private Submarine Submarine;
	[SerializeField] private PlayerSubmarineConfig Config;
	[SerializeField] private PlayerSubmarineModel model;

	private void Awake(){
		Init(new PlayerSubmarineModel(){
			Health = Config.Model.MaxHealth,
			Rooms = Config.Model.Rooms.Select(room => new RoomItem(){
				Type = room.Type,
				Model = new RoomModel(){
					Health = room.Model.MaxHealth,
					CrewModels = room.Model.CrewModels.Select(model => new CrewMemberModel(){
						Health = model.MaxHealth,
						Id = model.Id,
					}).ToList(),
				}
			}).ToList(),
		});
	}

	public void Init(PlayerSubmarineModel model){
		this.model = model;
	}

	public void Tick(){
		HealTick();
		RepairTick();
	}

	public void TakeDamage(float damage){
		model.Health = model.Health = Mathf.Max(model.Health - damage, 0.0f);
		if (model.Health <= 0.0f){
			OnDestroy?.Invoke();
		}
	}

	private void HealTick(){
		//todo
	}

	private void RepairTick(){
		//todo
//			model.Health = Mathf.Min(model.Health+x,model.MaxHealth);
	}
}