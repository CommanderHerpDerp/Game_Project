using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gardening : MonoBehaviour {
		private NavMeshAgent agent;
		private List<Vector3> orders;
		private Vector3 Homeposition;
		private List<Sapling> saplings;
		private int i=0;
		public float CollectTime =5;
		private float CollectTimer;
		public GameObject Tree;
		public class Forest
	{
		public Vector3 cornerA;
		public Vector3 cornerB;
		public float cornerC;
		public Forest(Vector3 CoA, Vector3 CoB, float Wid){
			cornerA=CoA;
			cornerB=CoB;
			cornerC=Wid;
		}
	}
		public class Sapling
	{
		public Vector3 position;
		public bool planned;
		public Sapling(Vector3 pos, bool pla)
		{
			position=pos;
			planned=pla;
		}
	}


// Use this for initialization
	void Start () {
		// This list is a manual position of the spalings generated
		Homeposition = transform.position;
		agent = GetComponent<NavMeshAgent>();
		agent.destination = Homeposition;
		orders = new List<Vector3> ();
		saplings = new List<Sapling> ();
		saplings.Add(new Sapling(new Vector3 (9,0,9),false));
		saplings.Add(new Sapling(new Vector3 (9,0,18),false));
		saplings.Add(new Sapling(new Vector3 (9,0,27),false));
		saplings.Add(new Sapling(new Vector3 (18,0,9),false));
		saplings.Add(new Sapling(new Vector3 (18,0,18),false));
		saplings.Add(new Sapling(new Vector3 (18,0,27),false));
		saplings.Add(new Sapling(new Vector3 (27,0,9),false));
		saplings.Add(new Sapling(new Vector3 (27,0,18),false));
		saplings.Add(new Sapling(new Vector3 (27,0,27),false));
	
			foreach(Sapling sap in saplings){
			if (!sap.planned){
				orders.Add(sap.position);
				sap.planned=true;

			}
				

		}
		                           
	}
	// Update is called once per frame
	void Update () {
		if (agent.remainingDistance < agent.stoppingDistance) {
			//move to next point or back to first if at the last point
			
			CollectTimer += Time.deltaTime;
			if (CollectTimer >= CollectTime){
				if(orders.Count!=0){
					if(i==0){
						SetOrdersForSaplings();
					}
					if(i>=1){
						GameObject newTree = Instantiate (Resources.Load (Tree.name)) as GameObject;
						newTree.transform.position=orders[i];
					}
					
					if(i == orders.Count-1)
						i=0;
					else
						i++;
					
					CollectTimer=0;
					
					//next destination
					agent.destination=orders[i];
				}
			}
			
		}
	}
	void SetOrdersForSaplings(){
	orders.Clear();
	orders.Add(Homeposition);
	foreach(Sapling sap in saplings){
			if(!sap.planned){
				orders.Add (sap.position);
				sap.planned =true;
		}
	}
}
}