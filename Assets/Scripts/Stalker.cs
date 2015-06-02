using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stalker : MonoBehaviour {
	private NavMeshAgent agent;
	private List<Vector3> orders;
	public float hungerTime=15;
	private float hungerClock;
	private GameObject currentAnimal;
	private float animalDistance;
	public Material changeMaterial1;
	public Material changeMaterial2;
	public Renderer rend;

	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		FindAnimalToKill ();
		}
	

void Update () {

		hungerClock += Time.deltaTime;

		if (currentAnimal == null)
			FindAnimalToKill ();
	
		if (currentAnimal) {
			animalDistance = Vector3.Distance (agent.transform.position, currentAnimal.transform.position);

			if (animalDistance > 15) {
				agent.speed = 3.5f;
				this.rend.material = changeMaterial1;
			}
		

		}
		if (hungerClock >= hungerTime) {
			agent.destination = currentAnimal.transform.position;

			if (animalDistance <= 15) {
				agent.speed = 7f;
				this.rend.material = changeMaterial2;
			}

			if (animalDistance < 2) {
				DestroyParentGameObject animalScript = currentAnimal.GetComponent<DestroyParentGameObject> ();
				animalScript.DestroyObj ();
				hungerClock = 0;
			}
		}
	}

	void FindAnimalToKill(){
		currentAnimal = FindNearestAnimal(250);
		if(currentAnimal!=null){	
			//orders.Clear();
			//agent.destination = currentAnimal.transform.position;
			print (currentAnimal.name);
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
