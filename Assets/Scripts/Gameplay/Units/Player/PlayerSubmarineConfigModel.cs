using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Units.Player {
	[Serializable]
	public class PlayerSubmarineConfigModel {
		public float MaxHealth;
		public float Damage;
		public List<RoomConfigItem> Rooms = new List<RoomConfigItem>();
	}
	
	[Serializable]
	public class RoomConfigItem {
		public RoomType Type;
		public RoomConfigModel Model;
	}
	
	[Serializable]
	public class RoomConfigModel {
		public float MaxHealth;
		public List<CrewMemberConfigModel> CrewModels;
	}

	[Serializable]
	public class CrewMemberConfigModel {
		public int Id;
		public float MaxHealth;
	}
}