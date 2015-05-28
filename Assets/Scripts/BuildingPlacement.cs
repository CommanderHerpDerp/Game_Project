using UnityEngine;
using System.Collections;

public class BuildingPlacement : MonoBehaviour {
	
	public float scrollSensitivity;
	
	private PlaceableBuilding placeableBuilding;
	private Transform currentBuilding;
	private bool hasPlaced;
	private RaycastHit hit;
	public Camera camera2;

	
	public LayerMask buildingsMask;
	private LayerMask terrainMask;
	
	private PlaceableBuilding placeableBuildingOld;
	void Start(){
		terrainMask = ~(1 << 2 | 1 << 8 | 1 << 9);


	}
	
	// Update is called once per frame
	void Update () {
		Ray p = camera2.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast (p, out hit, Mathf.Infinity,terrainMask)) {

			if (currentBuilding != null && !hasPlaced) {
			
				currentBuilding.position = hit.point;
			
				if (Input.GetMouseButtonDown (0)) {
					if (IsLegalPosition ()) {
						if(currentBuilding.GetComponent<SpawnWorker>()!=null){
							currentBuilding.GetComponent<SpawnWorker>().Spawn();
						}
						hasPlaced = true;	
					}
				}
			} else {
				if (Input.GetMouseButtonDown (0)) {;
					if (Physics.Raycast (p, out hit, Mathf.Infinity, buildingsMask)) {
						if (placeableBuildingOld != null) {
							placeableBuildingOld.SetSelected (false);
						}
						hit.collider.gameObject.GetComponent<PlaceableBuilding> ().SetSelected (true);
						placeableBuildingOld = hit.collider.gameObject.GetComponent<PlaceableBuilding> ();
					} else {
						if (placeableBuildingOld != null) {
							placeableBuildingOld.SetSelected (false);
						}
					}
				}
			}
		}
	}


	bool IsLegalPosition() {
		if (placeableBuilding.colliders.Count > 0) {
			return false;	
		}
		return true;
	}
	
	public void SetItem(GameObject b) {
		hasPlaced = false;
		currentBuilding = ((GameObject)Instantiate(b)).transform;
		placeableBuilding = currentBuilding.GetComponent<PlaceableBuilding>();
	}
}
