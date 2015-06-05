using UnityEngine;
using System.Collections;

public class SapletGrowth : MonoBehaviour {
	public GameObject Saplet;
	public float GrowthTime=5;
	private float GrowthTimer;
	private Vector3 plantspot;
	public GameObject Tree;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
			GrowthTimer += Time.deltaTime;
			Mathf.FloorToInt(GrowthTimer);
		if (GrowthTimer >= GrowthTime) {
			plantspot = transform.position;
			Destroy (Saplet);
			GameObject newTree = Instantiate (Resources.Load (Tree.name)) as GameObject;
			newTree.transform.position=plantspot;		
		}
				
	}
}
