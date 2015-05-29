using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gardening : MonoBehaviour {
		public GameObject Tree;

		public class Forest
	{
		public Vector3 cornerA;
		public Vector3 cornerB;
		public Forest(Vector3 CoA, Vector3 CoB){
			cornerA=CoA;
			cornerB=CoB;
		}
	}
		public class Sapling
	{
		public Vector3 position;
		public bool planted;
		public Sapling(Vector3 pos, bool pla)
		{
			position=pos;
			planted=pla;
		}
	}


// Use this for initialization
	void Start () {
		List<Sapling> saplings = new List<Sapling.pos> ();
		saplings.Add(new Sapling(9,0,9));
		saplings.Add(new Sapling(12,0,9));
		saplings.Add(new Sapling(15,0,9));

		foreach(Sapling pos in saplings){
			GameObject newTree = Instantiate (Resources.Load (Tree.name)) as GameObject;
			newTree.gameObject.transform.position.Set(Sapling.position);
		}
		                           
	}
	// Update is called once per frame
	void Update () {
		
	}
}
