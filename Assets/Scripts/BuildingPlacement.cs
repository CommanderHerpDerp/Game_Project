using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class BuildingPlacement : MonoBehaviour {
	
	public float scrollSensitivity;
	
	private PlaceableBuilding placeableBuilding;
	private Transform currentBuilding;
	private bool hasPlaced;
	private RaycastHit hit;
    public Camera camera2; 
	public LayerMask buildingsMask;
	private LayerMask terrainMask;
    private Color startingColor;
    private Color transpColor;
    private Color invalidColor;
	
	private PlaceableBuilding placeableBuildingOld;
	void Start(){
		terrainMask = ~(1 << 2 | 1 << 8 | 1 << 9 | 1 <<10);


	}
	
	// Update is called once per frame
	void Update () {

		Ray p = camera2.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast (p, out hit, Mathf.Infinity,terrainMask)) {

			if (currentBuilding != null && !hasPlaced) {
			
				currentBuilding.position = hit.point;
                if (IsLegalPosition())
					currentBuilding.GetComponentInChildren<Renderer>().material.color = transpColor;
                else
					currentBuilding.GetComponentInChildren<Renderer>().material.color = invalidColor;
			
				if (Input.GetMouseButtonDown (0)) {
					if (IsLegalPosition ()) {
						if(currentBuilding.GetComponent<SpawnWorker>()!=null){
							currentBuilding.GetComponent<SpawnWorker>().Spawn();
						}

                        currentBuilding.GetComponentInChildren<Renderer>().material.color = startingColor;
                        currentBuilding.GetComponent<NavMeshObstacle>().enabled = true;
						hasPlaced = true;	
					}
				}
			} else {
				if (Input.GetMouseButtonDown (0)){
					//if(!EventSystem.current.IsPointerOverGameObject()){
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
					//}
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
		startingColor = currentBuilding.GetComponentInChildren<Renderer>().material.color;
        transpColor = startingColor;
        transpColor.a=.7f;
        invalidColor=transpColor;
        invalidColor.r = invalidColor.r * 1.2f;
        invalidColor.g = invalidColor.g * .8f;
        invalidColor.b = invalidColor.b * .8f;
		currentBuilding.GetComponentInChildren<Renderer>().material.color = transpColor;
        currentBuilding.GetComponent<NavMeshObstacle>().enabled = false;
		placeableBuilding = currentBuilding.GetComponent<PlaceableBuilding>();
	}
}
