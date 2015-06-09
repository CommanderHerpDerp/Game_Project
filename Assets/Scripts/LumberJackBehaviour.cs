using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LumberJackBehaviour : MonoBehaviour
{
	private List<Vector3> destinations;
	private NavMeshAgent agent;
	private int i=0;
	public float collectTime =5;
	private float collectTimer;
	private GameObject TreeObj;
	private Vector3 homePosition;
	private bool hadFirstUpdate;
	private LumberJackInventory inventory;
	private WoodCutterInventory buildingInventory;


	// Use this for initialization
	void Start () {
		inventory = GetComponent<LumberJackInventory> ();
		buildingInventory = transform.parent.gameObject.GetComponent<WoodCutterInventory>();
		homePosition = transform.position;
		agent = GetComponent<NavMeshAgent>();
		destinations = new List<Vector3> ();
		destinations.Add (homePosition);
		agent.destination = homePosition;

		//hack to stop derpy moving on spawn
		hadFirstUpdate = false;
	}

	// Update is called once per frame
	void Update () {
		//hack to stop derpy moving on spawn
		if (!hadFirstUpdate) {
			if (Vector3.Distance(transform.position,homePosition)>2){
				print ("Fixed Derp");
			}
			hadFirstUpdate = true;
			transform.position = homePosition;
		}

		//check if at next point
		if (agent.remainingDistance < agent.stoppingDistance) {
			//move to next point or back to first if at the last point

			collectTimer += Time.deltaTime;
			if (collectTimer >= collectTime){
				if(destinations.Count!=0){
					if(i==0){
						inventory = GetComponent<LumberJackInventory> ();
						inventory.items["resource.wood"] = buildingInventory.Add(inventory.items["resource.wood"]);
						SetTargetsForTree();
					}
					if(i==1){
						if (inventory.ItemAllowed("resource.wood")){
							inventory = GetComponent<LumberJackInventory> ();
							if(inventory.items["resource.wood"].stackSize<inventory.items["resource.wood"].maxStackSize){
								Destroy (TreeObj);
								inventory.items["resource.wood"].stackSize++;
							}
							else{
								destinations.Clear ();
								destinations.Add (homePosition);
							}
						}
						else
							print ("Stupid idiot, I can't carry wood!");
					}

					if(i == destinations.Count-1)
						i=0;
					else
						i++;
				
					collectTimer=0;

					//next destination
					agent.destination=destinations[i];
				}
			}

		}
	}

	void SetTargetsForTree(){
		TreeObj = FindNearestTree(50);
		if(TreeObj!=null){	
			TreeObj.tag = "tree.tagged";
			print (TreeObj.name);
			destinations.Clear();
			destinations.Add(homePosition);
			destinations.Add(TreeObj.transform.position);
		}
		else
		{
			destinations.Clear();
			destinations.Add (homePosition);
		}
	}

	GameObject FindNearestTree(float radius){
		GameObject currentBest=null;
		float currentDist=radius+1;
		Collider[] treecolliders = Physics.OverlapSphere (transform.position, radius);
		foreach (Collider treeCollider in treecolliders)
		{
			if(treeCollider.gameObject.CompareTag("tree")){
				if (Vector3.Distance( treeCollider.gameObject.transform.position,homePosition)< currentDist){
					currentBest=treeCollider.gameObject;
					currentDist=Vector3.Distance( treeCollider.gameObject.transform.position,homePosition);
				}
			}
		
		}
		return currentBest;
	}
}






