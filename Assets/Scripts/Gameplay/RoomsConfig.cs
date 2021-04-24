using System;
using Gameplay.Units.Player;
using UnityEngine;

namespace Gameplay {
	[CreateAssetMenu(fileName = nameof(RoomsConfig), menuName = nameof(RoomsConfig), order = 0)]
	public class RoomsConfig : ScriptableObject {
		public WeaponsRoomConfig WeaponsRoomConfig;
		public SonarRoomConfig SonarRoomConfig;
		public EngineRoomConfig EngineRoomConfig;
		public MedBayRoomConfig MedBayRoomConfig;
		public HullRepairRoomConfig HullRepairRoomConfig;

		public float GetWeaponsRoomValue(int insideCrewCunt){
			return WeaponsRoomConfig.BaseValue + WeaponsRoomConfig.PerCrewMember * insideCrewCunt;
		}

		public float GetSonarRoomValue(int insideCrewCunt){
			return SonarRoomConfig.BaseValue + SonarRoomConfig.PerCrewMember * insideCrewCunt;
		}

		public float GetEngineRoomValue(int insideCrewCunt){
			return EngineRoomConfig.BaseValue + EngineRoomConfig.PerCrewMember * insideCrewCunt;
		}

		public float GetMedBayRoomValue(){
			return MedBayRoomConfig.BaseValue;
		}

		public float GetHullRepairRoomValue(int insideCrewCunt){
			return HullRepairRoomConfig.BaseValue + HullRepairRoomConfig.PerCrewMember * insideCrewCunt;
		}
	}

	[Serializable]
	public class WeaponsRoomConfig {
		public float BaseValue;
		public float PerCrewMember;
	}

	[Serializable]
	public class SonarRoomConfig {
		public float BaseValue;
		public float PerCrewMember;
	}

	[Serializable]
	public class EngineRoomConfig {
		public float BaseValue;
		public float PerCrewMember;
	}

	[Serializable]
	public class MedBayRoomConfig {
		public float BaseValue;
	}

	[Serializable]
	public class HullRepairRoomConfig {
		public float BaseValue;
		public float PerCrewMember;
	}
}