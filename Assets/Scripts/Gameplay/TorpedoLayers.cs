using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay {
	[CreateAssetMenu(fileName = nameof(TorpedoLayers), menuName = nameof(TorpedoLayers), order = 0)]
	public class TorpedoLayers : ScriptableObject {
		public List<TorpedoLayerLayerName> Layers;
	}
	
	[Serializable]
	public class TorpedoLayerLayerName {
		public TorpedoLayer Layer;
		public string Name;
	}
}