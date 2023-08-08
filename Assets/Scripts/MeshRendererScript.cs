using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshRendererScript : MonoBehaviour {

	public string layerName;
	public int order;

	private MeshRenderer rend;
	void Awake() {
		rend = GetComponent<MeshRenderer>();
		rend.sortingLayerName = layerName;
		rend.sortingOrder = order;
	}

	void Update() {
		if(rend.sortingLayerName != layerName)
			rend.sortingLayerName = layerName;

		if(rend.sortingOrder != order)
			rend.sortingOrder = order;
	}

	public void OnValidate() {
		rend = GetComponent<MeshRenderer>();
		rend.sortingLayerName = layerName;
		rend.sortingOrder = order;
	}
}