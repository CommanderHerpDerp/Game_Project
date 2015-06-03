using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stalker : MonoBehaviour {
	private NavMeshAgent agent;
	public class humans
	{
		public List<GameObject>humanTargets;
		public List<Vector3> humanTargetsPos;
		public float humanTargetsDist;
		public humans(List<GameObject> humanTs,List<Vector3> humanPs, float humanDs){
			humanTargets=humanTs;
			humanTargetsPos=humanPs;
			humanTargetsDist=humanDs;
			}
	}
	public List<humans> humansL;
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
				DestroyParentGameObject animalScript = currentAnimal.GetComponent<DestroyParentGameObject> ();
				animalScript.DestroyObj ();
				hungerClock = 0;
			}
		}
		if (hungerClock > (hungerTime*10)) {
			Rampage();
			//float humanDistance = Vector3.Distance (agent.transform.position,humansL);

		//	if (humanDistance < 2){
				//DestroyParentGameObject humanScript = humanTarget.GetComponent<DestroyParentGameObject> ();
		//		humanScript.DestroyObj ();
		//		hungerClock = 0;
		//	}
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

	void Rampage (){
		if (humansL.Count != 0) {
			this.rend.material=changeMaterial3;
			agent.speed=7f;
			if (i == 0) {

			}

			if (i == humansL.Count - 1)
				i = 0;
			else
				i++;
		//	agent.destination = humanTargetsPos [i];
		}
	}

	List<Vector3> FindHumans(){
		GameObject currentHuman;
		Vector3 currentHumanPos;
		float currentHumanDist;
		humansL = new List<humans> ();
		float currentDist = 500;
		Collider[] humanColliders = Physics.OverlapSphere (transform.position, 500);
		foreach (Collider humanCollider in humanColliders) {
			if (humanCollider.gameObject.CompareTag ("Humanoid")) {
				if (Vector3.Distance (humanCollider.gameObject.transform.position, transform.position) < currentDist) {
					currentHuman = humanCollider.gameObject;
					currentHumanPos = humanCollider.gameObject.transform.position;
					currentHumanDist = Vector3.Distance (currentHuman.transform.position, transform.position);
//					humansL.Add (new humans(currentHuman,currentHumanPos,currentHumanDist));
				}
			}

		}
		return null;
//		return humansL;
		}
}
