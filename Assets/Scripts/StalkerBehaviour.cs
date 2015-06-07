using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class StalkerBehaviour : MonoBehaviour {
	private NavMeshAgent agent;
	public class humans : IComparable<humans>
	{
		public GameObject humanTarget;
		public float humanTargetDist;
		public humans(GameObject humanTs, float humanDs){
			humanTarget=humanTs;
			humanTargetDist=humanDs;
		}
		public int CompareTo(humans other)
		{
			if(other == null)
			return 1;

			if(this.humanTargetDist> other.humanTargetDist)
			   return 1;

			if (this.humanTargetDist < other.humanTargetDist)
				return -1;
			else
				return 0;
		}
	}
	public float hungerTime=15;
	private float hungerClock;
	private float distClock;
	private GameObject currentAnimal;
	private GameObject curretHuman;
	private float animalDistance;
	public Material changeMaterial1;
	public Material changeMaterial2;
	public Material changeMaterial3;
	public Renderer rend;
	private int i=0;

	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		FindAnimalToKill ();
		}
	

void Update () {

		hungerClock += Time.deltaTime;

		if (currentAnimal == null)
			FindAnimalToKill ();
	
		float distRefresh = 5;
		distClock += Time.deltaTime;
		if (currentAnimal&& (distClock>=distRefresh)){
			animalDistance = Vector3.Distance (agent.transform.position, currentAnimal.transform.position);

			if (animalDistance > 15) {
				agent.speed = 3.5f;
				this.rend.material = changeMaterial1;
			}
		

		}
		if (hungerClock >= hungerTime && currentAnimal) {
			agent.destination = currentAnimal.transform.position;

			if (animalDistance <= 15) {
				agent.speed = 7f;
				this.rend.material = changeMaterial2;
			}

			if (animalDistance < 2) {
				Destroy (currentAnimal);
				hungerClock = 0;
			}
		}
		if (hungerClock > 30) {
			Rampage(FindHumans());
		}
}

	void FindAnimalToKill(){
		currentAnimal = FindNearestAnimal(250);
		if(currentAnimal!=null){	
			print (currentAnimal.name);
			}
		}

	GameObject FindNearestAnimal(float radius){
		GameObject currentAnimal=null;
		float currentDist=radius+1;
		Collider[] animalColliders = Physics.OverlapSphere (transform.position, radius);
		foreach (Collider animalCollider in animalColliders){
			if(animalCollider.gameObject.CompareTag("Animal")){
					if (Vector3.Distance( animalCollider.gameObject.transform.position,transform.position)< currentDist){
						currentAnimal=animalCollider.gameObject;
						currentDist=Vector3.Distance( animalCollider.gameObject.transform.position,transform.position);
						}
			}
			
		}
		return currentAnimal;
	}

	void Rampage (List<humans> humansL){
		if (humansL.Count != 0) {
			float humanDist = (Vector3.Distance (humansL [i].humanTarget.transform.position, transform.position));
			this.rend.material = changeMaterial3;
			agent.speed = 7f;
			float distRefresh = 5;
			distClock += Time.deltaTime;

			if (humanDist >= 2 && (distClock > distRefresh)) {
				agent.destination = humansL [i].humanTarget.transform.position;
				distClock=0;
				}

				if (humanDist < 2) {
				Destroy (humansL[i].humanTarget);
				if(i == humansL.Count-1)
					i=0;
				else
					i++;
				}
				
			}
		}

	List<humans> FindHumans(){
		GameObject currentHuman;
		float currentHumanDist;
		List<humans> humansL = new List<humans>();
		Collider[] humanColliders = Physics.OverlapSphere (transform.position, 500);
		foreach (Collider humanCollider in humanColliders) {
			if (humanCollider.gameObject.CompareTag ("Humanoid")) {
				currentHuman = humanCollider.gameObject;
				currentHumanDist = Vector3.Distance (currentHuman.transform.position, transform.position);
				humansL.Add (new humans(currentHuman, currentHumanDist));


			}

		}
		humansL.Sort ();
		return humansL;
	}
}
