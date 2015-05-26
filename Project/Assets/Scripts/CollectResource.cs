﻿// CollectResource.cs
// CollectResource.cs
using UnityEngine;

public class CollectResource : MonoBehaviour {

	public NavMeshAgent NavMeshAgent;
	public float dist;
	public float minDist = 10f;


	void Start()

		
	{
		// look in the scene for object named ''tree'' and set target destination as its position
		NavMeshAgent agent = this.GetComponent<NavMeshAgent>();
		agent.SetDestination(GameObject.Find("tree").transform.position);


		print ("Found Tree");

		{

				
				}
	}
	void Update(){
		// Check if we've reached the destination
		
		{
			
			NavMeshAgent agent = this.GetComponent<NavMeshAgent>();
			dist = agent.remainingDistance;
			print(dist);
			print ("Tada");
			
		}
	}
}


	
	
