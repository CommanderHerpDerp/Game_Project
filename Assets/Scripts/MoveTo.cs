// MoveToClickPoint.cs
using UnityEngine;

public class MoveTo : MonoBehaviour {
	private NavMeshAgent agent;
	
	void Start() {
		agent = GetComponent<NavMeshAgent>();
	}
	
	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit hit;
			
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit,Mathf.Infinity, 1<<0)) {
				agent.destination = hit.point;
			}
		}
	}
}

