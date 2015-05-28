using UnityEngine;
using System.Collections;

public class SpawnWorker : MonoBehaviour {
	
	public GameObject WorkerUnit;

	// Use this for initialization
	void Start () {
	
	}


	public void Spawn(){
		if (transform.Find ("DoorWay")!=null) {
			GameObject obj = Instantiate (Resources.Load (WorkerUnit.name)) as GameObject;
			obj.transform.position = transform.Find ("DoorWay").transform.position;
		}
	}
}
	

