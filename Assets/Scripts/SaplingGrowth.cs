using UnityEngine;
using System.Collections;

public class SaplingGrowth : MonoBehaviour {
	public GameObject sapling;
	public float growthTime=5;
	private float growthTimer;
	private Vector3 plantspot;
	public GameObject tree;
			
	// Update is called once per frame
	void Update () {
			growthTimer += Time.deltaTime;
			Mathf.FloorToInt(growthTimer);
		if (growthTimer >= growthTime) {
			plantspot = transform.position;
			Destroy (sapling);
			GameObject newTree = Instantiate (Resources.Load (tree.name)) as GameObject;
			newTree.transform.position=plantspot;		
		}
				
	}
}
