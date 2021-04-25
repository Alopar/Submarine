using System;
using System.Collections.Generic;

namespace Gameplay.Units.Player {
	[Serializable]
	public class PlayerSubmarineConfigModel {
		public float MaxHealth;
		public float BaseDamage;
		public float DamageMultiplierPerCrewMember;
		public List<RoomConfigItem> Rooms = new List<RoomConfigItem>();
		public RoomsConfig RoomsConfig;
	}
	
	[Serializable]
	public class RoomConfigItem {
		public RoomType Type;
		public RoomConfigModel Model;
	}
	
	[Serializable]
	public class RoomConfigModel {
		public float MaxHealth;
		public float RepairingSpeedPerCrewMember;
		public List<CrewMemberConfigModel> CrewModels;
	}

	[Serializable]
	public class CrewMemberConfigModel {
		public int Id;
		public float MaxHealth;
	}
}