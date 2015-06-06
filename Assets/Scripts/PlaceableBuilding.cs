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
			string tempName;

			if (bName =="")
				tempName = name;
			else
				tempName = bName;
			GUI.Button(new Rect(Screen.width /2-100, Screen.height / 20, 100, 30),tempName);

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
		GetComponent<Inventory> ().drawInv = s;
	}

	
}
