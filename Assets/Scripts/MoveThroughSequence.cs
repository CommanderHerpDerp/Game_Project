using UnityEngine;
using System.Collections;

public class MoveThroughSequence : MonoBehaviour
{
	Vector3[] targets;
	NavMeshAgent agent;
	int i=0;
	public Transform marker;
	public float CollectTime =5;
	private float CollectTimer;


	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		targets = new Vector3[2];
		targets[0] = GameObject.FindWithTag("tree").transform.position;
		print ("Found Tree");
		targets [1] = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//check if at next point
		if (agent.remainingDistance < agent.stoppingDistance) {
			//move to next point or back to first if at the last point

			CollectTimer += Time.deltaTime;
			if (CollectTimer >= CollectTime){
				GameObject TreeObj = GameObject.FindWithTag ("tree");
				if(TreeObj!=null){
					if (i==0){
						DestroyParentGameObject TreeObjScript=TreeObj.GetComponent<DestroyParentGameObject>();
						TreeObjScript.DestroyObj();
					}
					if(i == targets.Length-1){
						i=0;
							targets[0] = TreeObj.transform.position;
							print ("Found Tree");
							targets [1] = transform.position;

						}
					else
						i++;

					CollectTimer=0;
					}
				}



			//next destination
			agent.destination=targets[i];
		}
	}





	void GenerateTestPoints (){

		//Generate test movement points and put a marker 4 units above each point.
		int testRadius = 15;
		int testPoints = 5;
		targets = new Vector3[testPoints];
		for (int a=0; a<testPoints; a++) {
			targets[a]=new Vector3 (testRadius * Mathf.Cos(2*a * Mathf.PI /testPoints),0,testRadius * Mathf.Sin(2*a * Mathf.PI /testPoints));
			Instantiate (marker, targets[a] + new Vector3(0,4,0), Quaternion.identity);
		}
		agent.destination = targets [i];
}
}






