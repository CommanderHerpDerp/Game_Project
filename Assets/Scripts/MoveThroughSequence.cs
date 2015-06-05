using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveThroughSequence : MonoBehaviour
{
	private List<Vector3> targets;
	private NavMeshAgent agent;
	private int i=0;
//	public Transform marker;
	public float CollectTime =5;
	private float CollectTimer;
	private GameObject TreeObj;
	private Vector3 HomePosition;
	private bool HadFirstUpdate;


	// Use this for initialization
	void Start () {
		HomePosition = transform.position;
		agent = GetComponent<NavMeshAgent>();
		targets = new List<Vector3> ();
		targets.Add (HomePosition);
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
				if(targets.Count!=0){
					if(i==0){
						SetTargetsForTree();
					}
					if(i==1){
						Destroy (TreeObj);
					}

					if(i == targets.Count-1)
						i=0;
					else
						i++;
				
					CollectTimer=0;

					//next destination
					agent.destination=targets[i];
				}
			}

		}
	}

	void SetTargetsForTree(){
		TreeObj = FindNearestTree(50);
		if(TreeObj!=null){	
			TreeObj.tag = "tree.tagged";
			print (TreeObj.name);
			targets.Clear();
			targets.Add(HomePosition);
			targets.Add(TreeObj.transform.position);
		}
		else
		{
			targets.Clear();
			targets.Add (HomePosition);
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
			targets.Add(new Vector3 (testRadius * Mathf.Cos(2*a * Mathf.PI /testPoints),0,testRadius * Mathf.Sin(2*a * Mathf.PI /testPoints)));
//			Instantiate (marker, targets[a] + new Vector3(0,4,0), Quaternion.identity);
		}
		agent.destination = targets [i];
	}
}






