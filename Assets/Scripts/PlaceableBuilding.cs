using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaceableBuilding : MonoBehaviour {
	
	[HideInInspector]
	public List<Collider> colliders = new List<Collider>();
	private bool isSelected;
	public string title;
	public string bName;

	void OnGUI() {
		if (isSelected) {
			GUI.Button(new Rect(Screen.width /2, Screen.height / 20, 100, 30), bName);
			if (bName =="") {
				GUI.Button(new Rect(Screen.width /2, Screen.height / 20, 100, 30),name);
			}
		}
		
	}
	
	void OnTriggerEnter(Collider c) {
		if (c.tag == "Building") {
			colliders.Add(c);	
		}
	}
	
	void OnTriggerExit(Collider c) {
		if (c.tag == "Building") {
			colliders.Remove(c);	
		}
	}
	
	public void SetSelected(bool s) {
		isSelected = s;	
	}

	
}
