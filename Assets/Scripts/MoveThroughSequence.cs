using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveThroughSequence : MonoBehaviour
{
	List<Vector3> targets;
	NavMeshAgent agent;
	int i=0;
	public Transform marker;
	public float CollectTime =5;
	private float CollectTimer;
	private GameObject TreeObj;


	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		targets = new List<Vector3> ();
		TreeObj = GameObject.FindWithTag ("tree");
		if (TreeObj != null) {
			targets.Add(TreeObj.transform.position);
			print ("Found Tree");
			targets.Add(transform.position);
			agent.destination=targets[0];
			TreeObj.tag = "tree.tagged";
		}
	}
	
	// Update is called once per frame
	void Update () {
		//check if at next point
		if (agent.remainingDistance < agent.stoppingDistance) {
			//move to next point or back to first if at the last point

			CollectTimer += Time.deltaTime;
			if (CollectTimer >= CollectTime){

				if(i == targets.Count-1){
					i=0;
					TreeObj = GameObject.FindWithTag ("tree");
					if(TreeObj!=null){	
						targets.Add(TreeObj.transform.position);
						print ("Found Tree");
						targets.Add(transform.position);
					}
					else
						targets.Clear();

				}
				else
						i++;

				if(i==1){
					DestroyParentGameObject TreeObjScript=TreeObj.GetComponent<DestroyParentGameObject>();
					TreeObjScript.DestroyObj();
				}

				CollectTimer=0;
			}



			if( targets.Count>0){
					//next destination
					agent.destination=targets[i];
			}

		}
	}





	void GenerateTestPoints (){

		//Generate test movement points and put a marker 4 units above each point.
		int testRadius = 15;
		int testPoints = 5;
		for (int a=0; a<testPoints; a++) {
			targets.Add(new Vector3 (testRadius * Mathf.Cos(2*a * Mathf.PI /testPoints),0,testRadius * Mathf.Sin(2*a * Mathf.PI /testPoints)));
			Instantiate (marker, targets[a] + new Vector3(0,4,0), Quaternion.identity);
		}
		agent.destination = targets [i];
}
}






