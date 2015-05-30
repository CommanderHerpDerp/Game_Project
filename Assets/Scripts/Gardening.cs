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
	public GameObject Saplet;
	public GameObject marker;
	private Forest forest;
	
	public class Forest
	{
		public Vector3 cornerA;
		public Vector3 cornerB;
		public float Width;
		public Forest(Vector3 CoA, Vector3 CoB, float Wid){
			cornerA=CoA;
			cornerB=CoB;
			Width=Wid;
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
		agent = GetComponent<NavMeshAgent> ();
		agent.destination = Homeposition;
		orders = new List<Vector3> ();
		orders.Add (Homeposition);
		//forest = new Forest (new Vector3 (20, 0, 20), new Vector3 (30, 0, 27), 20);
		forest = new Forest (new Vector3((transform.position.x-20),(transform.position.y),(transform.position.z-20)), (new Vector3((transform.position.x-20),(transform.position.y),(transform.position.z+20))),40);
		saplings = new List<Sapling> ();
		int length = Mathf.CeilToInt (Vector3.Distance (forest.cornerA, forest.cornerB) / 3);
		int width = Mathf.CeilToInt (forest.Width / 3);
		Vector3 lengthUnitVector = Vector3.Normalize (forest.cornerB - forest.cornerA);
		Vector3 widthUnitVector = Vector3.Normalize (Vector3.Cross (forest.cornerB - forest.cornerA + new Vector3 (0, 1, 0), forest.cornerB - forest.cornerA));
		for (int a=0; a<width; a++) {
			for (int b=0; b<length; b++) {
				saplings.Add (new Sapling (forest.cornerA + b * lengthUnitVector * 3 + a * widthUnitVector * 5, false));
			}
		}
		GameObject c1 = Instantiate(marker) as GameObject;
		GameObject c2 = Instantiate(marker) as GameObject;
		GameObject c3 = Instantiate(marker) as GameObject;
		c1.transform.position = forest.cornerA+new Vector3(0,4,0);
		c2.transform.position = forest.cornerB+new Vector3(0,4,0);
		c3.transform.position = forest.cornerB+forest.Width*widthUnitVector+new Vector3(0,4,0);
	         
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
						GameObject newTree = Instantiate (Resources.Load (Saplet.name)) as GameObject;
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