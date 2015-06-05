using UnityEngine;
using System.Collections;

public class BuildingManager : MonoBehaviour {
	
	public GameObject[] buildings;
	private BuildingPlacement buildingPlacement;

	// Use this for initialization
	void Start () {
		buildingPlacement = GetComponent<BuildingPlacement>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		for (int i = 0; i <buildings.Length; i ++) {
			string butname;
			int bldname;
			PlaceableBuilding buildingscript = buildings[i].GetComponent<PlaceableBuilding>();
			if (buildingscript.title=="")
				butname = buildings[i].name;
			else
				butname = buildingscript.title;
			bldname=(buildings[i].name.Length)*5;
			 
			if (GUI.Button(new Rect(Screen.width/10,Screen.height/20 + Screen.height/12 * i,bldname,40), butname)) {
				buildingPlacement.SetItem(buildings[i]);
			}
		}
	}
}
