using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stalker : MonoBehaviour {
	private NavMeshAgent agent;
	private List<Vector3> orders;
	private Vector3 Homeposition;
	private float Hungertime=5;
	private float Hungerclock;
	private GameObject currentanimal;
	private int i=0;
	private bool HadFirstUpdate;

	// Use this for initialization
	void Start () {
		Homeposition = transform.position;
		agent = GetComponent<NavMeshAgent> ();
		agent.destination = Homeposition;
		orders = new List<Vector3> ();
		orders.Add (Homeposition);
	}
	
	// Update is called once per frame
	void Update () {
		//hack to stop derpy moving on spawn
		if (!HadFirstUpdate) {
			if (Vector3.Distance (transform.position, Homeposition) > 2) {
				print ("Fixed Derp");
			}
			HadFirstUpdate = true;
			transform.position = Homeposition;
		}
		
		//check if at next point
		if (agent.remainingDistance < agent.stoppingDistance) {
			//move to next point or back to first if at the last point

			Hungerclock += Time.deltaTime;
			if (Hungertime >= Hungerclock) {
				if (orders.Count != 0) {
					if (i == 0) {
						FindAnimalToKill ();
					}
					if (i == 1) {
						DestroyParentGameObject HuntScript = currentanimal.GetComponent<DestroyParentGameObject> ();
						HuntScript.DestroyObj ();
						Hungerclock = 0;
					}
				
					if (i == orders.Count - 1)
						i = 0;
					else
						i++;
				

					//next destination
					agent.destination = orders [i];
				}
			}
		}
	}
		void FindAnimalToKill(){
		currentanimal = FindNearestAnimal(250);
		if(currentanimal!=null){	
			currentanimal.tag = "Animal.tagged";
			print (currentanimal.name);
			orders.Clear();
			orders.Add(Homeposition);
			orders.Add(currentanimal.transform.position);
		}
		else
		{
			orders.Clear();
			orders.Add (Homeposition);
		}
	}
	GameObject FindNearestAnimal(float radius){
		GameObject currentanimal=null;
		float currentDist=radius+1;
		Collider[] animalColliders = Physics.OverlapSphere (transform.position, radius);
		foreach (Collider animalCollider in animalColliders)
		{
			if(animalCollider.gameObject.CompareTag("Animal")){
				if(!(animalCollider.gameObject == gameObject)){
					if (Vector3.Distance( animalCollider.gameObject.transform.position,transform.position)< currentDist){
						currentanimal=animalCollider.gameObject;
						currentDist=Vector3.Distance( animalCollider.gameObject.transform.position,transform.position);
						
					}
				}
			}
			
		}
		return currentanimal;
	}

}
