using UnityEngine;
using System.Collections;

public class SpawnWC : MonoBehaviour {
	

	// Use this for initialization
	void Start () {
	
	GameObject obj = Instantiate(Resources.Load("WoodCutter")) as GameObject;
	obj.transform.parent = transform;

	}
}
	

