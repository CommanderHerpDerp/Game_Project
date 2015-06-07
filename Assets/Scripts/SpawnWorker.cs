using UnityEngine;
using System.Collections;

public class SpawnWorker : MonoBehaviour {
	
	public GameObject WorkerUnit;

	public void Spawn(){
		if (transform.Find ("DoorWay")!=null) {
			GameObject obj = Instantiate (Resources.Load (WorkerUnit.name)) as GameObject;
			obj.transform.position = transform.Find ("DoorWay").transform.position;
			obj.transform.parent = transform;
		}
	}
}
	

