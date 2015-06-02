using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stalker : MonoBehaviour {
	private NavMeshAgent agent;
	private List<Vector3> orders;
	private Vector3 homePosition;
	private float hungerTime=5;
	private float hungerClock;
	private GameObject currentAnimal;
	private int i=0;
	private bool hadFirstUpdate;
	private bool animalClose;
	private float animalDistance;

	// Use this for initialization
	void Start () {
		homePosition = transform.position;
		agent = GetComponent<NavMeshAgent> ();
		orders = new List<Vector3> ();
		FindAnimalToKill ();
		agent.destination = orders[0];
	}
	
	// Update is called once per frame
	void Update () {
				
		//check if at next point
		if (agent.remainingDistance < agent.stoppingDistance) {
			//move to next point or back to first if at the last point
			hungerClock += Time.deltaTime;


		

			if (hungerClock >= hungerTime) {
				if(currentAnimal==null)
					FindAnimalToKill();
				if (orders.Count != 0) {
					if (i == 0) {
						animalDistance = Vector3.Distance (agent.transform.position, currentAnimal.transform.position);
						if (animalDistance<2){
							DestroyParentGameObject animalScript = currentAnimal.GetComponent<DestroyParentGameObject> ();
							animalScript.DestroyObj ();
						}
					}
				
					if (i == orders.Count - 1)
						i = 0;
					else
						i++;
				

					//next destination
					hungerClock = 0;
					agent.destination = orders [i];
				}
				FindAnimalToKill();
			}
		}
	}
		void FindAnimalToKill(){
		currentAnimal = FindNearestAnimal(250);
		if(currentAnimal!=null){	
			//currentAnimal.tag = "Animal.tagged";
			print (currentAnimal.name);
			orders.Clear();
			//orders.Add(homePosition);
			orders.Add(currentAnimal.transform.position);
			agent.speed=7f;
		}
		else
		{
			orders.Clear();
			//orders.Add (homePosition);
			agent.speed=3.5f;
		}
	}
	GameObject FindNearestAnimal(float radius){
		GameObject currentAnimal=null;
		float currentDist=radius+1;
		Collider[] animalColliders = Physics.OverlapSphere (transform.position, radius);
		foreach (Collider animalCollider in animalColliders)
		{
			if(animalCollider.gameObject.CompareTag("Animal")){
					if (Vector3.Distance( animalCollider.gameObject.transform.position,transform.position)< currentDist){
						currentAnimal=animalCollider.gameObject;
						currentDist=Vector3.Distance( animalCollider.gameObject.transform.position,transform.position);
												
					}
			}
			
		}
		return currentAnimal;
	}

}
