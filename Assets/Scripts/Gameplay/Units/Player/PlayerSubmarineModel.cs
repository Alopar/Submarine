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