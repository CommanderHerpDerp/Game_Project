using UnityEngine;
using System.Collections;

public class SpawnWC : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}


	public void SpawnWorker(){
		GameObject obj = Instantiate(Resources.Load("WoodCutter")) as GameObject;
		obj.transform.position = transform.Find ("DoorWay").transform.position;
	}
}
	

