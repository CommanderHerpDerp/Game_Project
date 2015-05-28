using UnityEngine;
using System.Collections;

public class Forester : MonoBehaviour {

	public float walkRadius = 10;
	NavMeshAgent agent;
	public float walkradius = 15;
	Vector3[] targets;
	public Vector3 finalposition;


	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		targets = new Vector3[2];
		targets [0] = finalposition;
		Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
		randomDirection += transform.position;
		NavMeshHit hit;
		NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
		Vector3 finalPosition = hit.position;
		agent.destination = finalPosition;
	}




	void Update() {

	}
}


