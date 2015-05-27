using UnityEngine;
using System.Collections;

public class MoveThroughSequence : MonoBehaviour
{
	Vector3[] targets;
	NavMeshAgent agent;
	int i=0;
	public Transform marker;



	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();


		//Generate test movement points and put a marker 4 units above each point.
		int testRadius = 15;
		int testPoints = 5;
		targets = new Vector3[testPoints];
		for (int a=0; a<testPoints; a++) {
			targets[a]=new Vector3 (testRadius * Mathf.Cos(2*a * Mathf.PI /testPoints),0,testRadius * Mathf.Sin(2*a * Mathf.PI /testPoints));
			Instantiate (marker, targets[a] + new Vector3(0,4,0), Quaternion.identity) as GameObject;
		}

		agent.destination = targets [i];
	}
	
	// Update is called once per frame
	void Update () {
		//check if at next point
		if (agent.remainingDistance < agent.stoppingDistance) {
			//move to next point or back to first if at the last point
			if(i == targets.Length-1)
				i=0;
			else
				i++;

			//next destination
			agent.destination=targets[i];
		}
	}
}
