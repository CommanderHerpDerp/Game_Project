using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimalRoam : MonoBehaviour {
	public float radius;
	private Vector3 spawnPosition;
	private NavMeshAgent agent;
	private float stationaryTimer;
	public float stationaryMax;
	private float stationaryCurrent;
	private List<Vector3> destinations;
	private int i=0;

	// Use this for initialization
	void Start () {
		spawnPosition = transform.position;
		agent = GetComponent<NavMeshAgent> ();
		destinations = new List<Vector3>();
		destinations.Add (spawnPosition);
		agent.destination = spawnPosition;
		stationaryCurrent = stationaryMax;
	}
	
	// Update is called once per frame
	void Update () {
		//if at current destination
		if (agent.remainingDistance < agent.stoppingDistance) {

			//advance stationary timer & if been stationary long enough
			stationaryTimer += Time.deltaTime;
			if (stationaryTimer >= stationaryCurrent){
				if(destinations.Count!=0){
					if(i==0){
						GameObject nearestAnimal = FindNearestAnimal();
						if (Mathf.FloorToInt(Random.value*50)==0||nearestAnimal == null){
							Vector2 rand = Random.insideUnitCircle*radius;
							destinations.Clear ();
							destinations.Add ( new Vector3(rand.x,0,rand.y));
						}
						else{
							destinations.Clear ();
							destinations.Add(nearestAnimal.transform.position);
						}

						stationaryCurrent = Random.value*stationaryMax;
					}

					if(i == destinations.Count-1)
						i=0;
					else
						i++;
					
					stationaryTimer=0;
					
					//next destination
					agent.destination=destinations[i];
				}
			}
			
		}
	
	}
	GameObject FindNearestAnimal(){
		GameObject currentBest=null;
		float currentDist=radius+1;
		Collider[] animalColliders = Physics.OverlapSphere (transform.position, radius);
		foreach (Collider animalCollider in animalColliders)
		{
			if(animalCollider.gameObject.CompareTag("Animal")){
				if(!(animalCollider.gameObject == gameObject)){
					if (Vector3.Distance( animalCollider.gameObject.transform.position,transform.position)< currentDist){
						currentBest=animalCollider.gameObject;
						currentDist=Vector3.Distance( animalCollider.gameObject.transform.position,transform.position);
					
					}
				}
			}
			
		}
		return currentBest;
}
}
