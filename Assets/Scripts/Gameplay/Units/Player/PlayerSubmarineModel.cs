using System;
using System.Collections.Generic;
using Gameplay.Units.Player;
using UnityEngine;

namespace Gameplay.Units.Player {
	[Serializable]
	public class PlayerSubmarineModel {
		public float Health;
		public List<RoomItem> Rooms;
	}

	[Serializable]
	public class RoomItem {
		public RoomType Type;
		public RoomModel Model;
	}

	[Serializable]
	public class RoomModel {
		public float Health;
		public bool IsActive;

		public List<CrewMemberModel> CrewModels;
	}

	[Serializable]
	public class CrewMemberModel {
		public int Id;
		public float Health;
	}
}
//
//public class RoomsDictionary:UnitySerializedDictionary<RoomType,RoomModel>{}
//
//public abstract class UnitySerializedDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
//{
//	[SerializeField, HideInInspector]
//	private List<TKey> keyData = new List<TKey>();
//	
//	[SerializeField, HideInInspector]
//	private List<TValue> valueData = new List<TValue>();
//
//	void ISerializationCallbackReceiver.OnAfterDeserialize()
//	{
//		this.Clear();
//		for (int i = 0; i < this.keyData.Count && i < this.valueData.Count; i++)
//		{
//			this[this.keyData[i]] = this.valueData[i];
//		}
//	}
//
//	void ISerializationCallbackReceiver.OnBeforeSerialize()
//	{
//		this.keyData.Clear();
//		this.valueData.Clear();
//
//		foreach (var item in this)
//		{
//			this.keyData.Add(item.Key);
//			this.valueData.Add(item.Value);
//		}
//	}
//}

//namespace Gameplay.Units.Player {
//	[Serializable]
//	public class PlayerSubmarineModel {
//		public float Health{ get; set; }
//		[SerializeField] private float MaxHealth;
//		[SerializeField] private float Damage;
//
//		[SerializeField] private List<RoomItem> Rooms = new List<RoomItem>();
//
////		public Dictionary<RoomType, Room> Rooms;
////
////		public PlayerSubmarineModel(){
////			Rooms = new Dictionary<RoomType, Room>(){
////				{RoomType.Weapons, new Room()},
////				{RoomType.Sonar, new Room()},
////				{RoomType.Engine, new Room()},
////				{RoomType.MedBay, new Room()},
////				{RoomType.HullRepair, new Room()},
////			};
////		}
//
////		public PlayerSubmarineModel(float health, float maxHealth, List<RoomItem> rooms){
////			Health = health;
////			MaxHealth = maxHealth;
////			Rooms = rooms;
////		}
//
//		public PlayerSubmarineModel Clone(){
//			return new PlayerSubmarineModel(){
//				Health = MaxHealth,
//				MaxHealth = MaxHealth,
//				Rooms = Rooms.Select(room => room.Clone()).ToList(),
//			};
//		}
//	}
//
//	[Serializable]
//	public class RoomItem {
//		[SerializeField] private RoomType Type;
//		[SerializeField] private RoomModel Model;
//
//		public RoomItem Clone(){
//			return new RoomItem(){
//				Type = Type,
//				Model = Model.Clone(),
//			};
//		}
//	}
//
//	public enum RoomType {
//		Weapons,
//		Sonar,
//		Engine,
//		MedBay,
//		HullRepair,
//	}
//
//	[Serializable]
//	public class RoomModel {
//		public float Health{ get; set; }
//		[SerializeField] private float MaxHealth;
//		public bool IsActive;
//
//		public List<CrewMemberModel> CrewModels;
//
////		public RoomModel(float maxHealth, float health, bool isActive, List<CrewMemberModel> crewModels){
////			MaxHealth = maxHealth;
////			Health = health;
////			IsActive = isActive;
////			CrewModels = crewModels;
////		}
//		public RoomModel Clone(){
//			return new RoomModel(){
//				Health = MaxHealth,
//				MaxHealth = MaxHealth,
//				IsActive = true,
//				CrewModels = CrewModels.Select(model => model.Clone()).ToList(),
//			};
//		}
//	}
//
//	[Serializable]
//	public class CrewMemberModel {
//		public int Id;
//		public float Health{ get; set; }
//		[SerializeField] private float MaxHealth;
//
////		public CrewMemberModel(int id, float maxHealth, float health){
////			Id = id;
////			MaxHealth = maxHealth;
////			Health = health;
////		}
//		public CrewMemberModel Clone(){
//			return new CrewMemberModel(){
//				Id = Id,
//				Health = MaxHealth,
//				MaxHealth = MaxHealth,
//			};
//		}
//	}
