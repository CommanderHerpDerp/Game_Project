using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WoodCutter : MonoBehaviour
{
	private List<Vector3> destinations;
	private NavMeshAgent agent;
	private int i=0;
//	public Transform marker;
	public float CollectTime =5;
	private float CollectTimer;
	private GameObject TreeObj;
	private Vector3 HomePosition;
	private bool HadFirstUpdate;
	private Inventory inventory;
	private Inventory parentInv;


	// Use this for initialization
	void Start () {
		inventory = GetComponent<Inventory> ();
		parentInv = transform.parent.gameObject.GetComponent<Inventory>();
		HomePosition = transform.position;
		agent = GetComponent<NavMeshAgent>();
		destinations = new List<Vector3> ();
		destinations.Add (HomePosition);
		agent.destination = HomePosition;

		//hack to stop derpy moving on spawn
		HadFirstUpdate = false;
	}

	// Update is called once per frame
	void Update () {
		//hack to stop derpy moving on spawn
		if (!HadFirstUpdate) {
			if (Vector3.Distance(transform.position,HomePosition)>2){
				print ("Fixed Derp");
			}
			HadFirstUpdate = true;
			transform.position = HomePosition;
		}

		//check if at next point
		if (agent.remainingDistance < agent.stoppingDistance) {
			//move to next point or back to first if at the last point

			CollectTimer += Time.deltaTime;
			if (CollectTimer >= CollectTime){
				if(destinations.Count!=0){
					if(i==0){
						if (!(inventory.slots[0]==null)){
								int slot;
								inventory.slots[0] = parentInv.Add(inventory.slots[0],out slot);
							}
						SetTargetsForTree();
					}
					if(i==1){
						if(inventory.slots[0]==null){
							Destroy (TreeObj);
							ItemManager.ItemStack wood = new ItemManager.ItemStack(ItemManager.items["Wood.Oak"]);
							wood.stackSize = 1;
							int slot;
							inventory.Add(wood, out slot);
						}
						else if(inventory.slots[0].stackSize<inventory.slots[0].maxStackSize){
							Destroy (TreeObj);
							inventory.slots[0].stackSize++;
						}
						else{
							destinations.Clear ();
							destinations.Add (HomePosition);
						}
					}

					if(i == destinations.Count-1)
						i=0;
					else
						i++;
				
					CollectTimer=0;

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
			destinations.Add(HomePosition);
			destinations.Add(TreeObj.transform.position);
		}
		else
		{
			destinations.Clear();
			destinations.Add (HomePosition);
		}
	}

	GameObject FindNearestTree(float radius){
		GameObject currentBest=null;
		float currentDist=radius+1;
		Collider[] treecolliders = Physics.OverlapSphere (transform.position, radius);
		foreach (Collider treeCollider in treecolliders)
		{
			if(treeCollider.gameObject.CompareTag("tree")){
				if (Vector3.Distance( treeCollider.gameObject.transform.position,HomePosition)< currentDist){
					currentBest=treeCollider.gameObject;
					currentDist=Vector3.Distance( treeCollider.gameObject.transform.position,HomePosition);

				}
			}
		
		}
		return currentBest;
}



	void GenerateTestPoints (){

		//Generate test movement points and put a marker 4 units above each point.
		int testRadius = 15;
		int testPoints = 5;
		for (int a=0; a<testPoints; a++) {
			destinations.Add(new Vector3 (testRadius * Mathf.Cos(2*a * Mathf.PI /testPoints),0,testRadius * Mathf.Sin(2*a * Mathf.PI /testPoints)));
//			Instantiate (marker, targets[a] + new Vector3(0,4,0), Quaternion.identity);
		}
		agent.destination = destinations [i];
	}
}






