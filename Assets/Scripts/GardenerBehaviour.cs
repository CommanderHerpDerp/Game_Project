using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GardenerBehaviour : MonoBehaviour {
	private NavMeshAgent agent;
	private List<Vector3> destinations;
	private Vector3 homePosition;
	private List<Sapling> saplings;
	private int i=0;
	public float collectTime =5;
	private float collectTimer;
	public GameObject sapling;
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
	
	
	void Start () {
		homePosition = transform.position;
		agent = GetComponent<NavMeshAgent> ();
		agent.destination = homePosition;
		//this will set a list of locations of saplets for the gardener to go and plant at
		destinations = new List<Vector3> ();
		destinations.Add (homePosition);
		//This will now decide where the forest is going to be created
		Vector3 coForestA = transform.position + new Vector3 (-25, 0, -10);
		Vector3 coForestB = transform.position + new Vector3 (-25, 0, 10);
		float coForestW = 20;
		saplings = new List<Sapling> ();
		int treesL = Mathf.CeilToInt (Vector3.Distance (coForestA, coForestB) / 5);
		int treesW = Mathf.CeilToInt (coForestW / 5);
		Vector3 lengthUnitVector = Vector3.Normalize (coForestB - coForestA);
		Vector3 widthUnitVector = Vector3.Normalize (Vector3.Cross (coForestB - coForestA + new Vector3 (0, 1, 0), coForestB - coForestA));
		float coOffsetX = (coForestW - (treesW - 1) * 5) / 2;
		float coOffsetZ = (Vector3.Distance (coForestA,coForestB)- (treesL-1)*5)/2;
		forest = new Forest (coForestA, coForestB, coForestW);
		for (int a=0; a<treesW; a++) {
			for (int b=0; b<treesL; b++) {
				saplings.Add (new Sapling (forest.cornerA + b * lengthUnitVector * 5 + a * widthUnitVector * 5+ new Vector3(coOffsetX,0,coOffsetZ), false));
			}
		}
		GameObject c1 = Instantiate(marker) as GameObject;
		GameObject c2 = Instantiate(marker) as GameObject;
		GameObject c3 = Instantiate(marker) as GameObject;
		GameObject c4 = Instantiate(marker) as GameObject;
		c1.transform.position = forest.cornerA+new Vector3(0,4,0);
		c2.transform.position = forest.cornerB+new Vector3(0,4,0);
		c3.transform.position = forest.cornerA+forest.Width*widthUnitVector+new Vector3(0,4,0);
		c4.transform.position = forest.cornerB+forest.Width*widthUnitVector+new Vector3(0,4,0);
	}

	// Update is called once per frame
	void Update () {
		if (agent.remainingDistance < agent.stoppingDistance) {
			//move to next point or back to first if at the last point
			
			collectTimer += Time.deltaTime;
			if (collectTimer >= collectTime){
				if(destinations.Count!=0){
					if(i==0){
						foreach (Sapling sap in saplings){sap.planned=false;}
						SetOrdersForSaplings();
					}
					if(i>=1){
						GameObject newTree = Instantiate (Resources.Load (sapling.name)) as GameObject;
						newTree.transform.position=destinations[i];
					}
					
					if(i == destinations.Count-1)
						i=0;
					else
						i++;
					
					collectTimer=0;
					
					//next destination
					agent.destination=destinations[i];
				}
			}
			
		}
	}
	void SetOrdersForSaplings(){
		destinations.Clear();
		destinations.Add(homePosition);
		foreach (Sapling sap in saplings) {
			if (!sap.planned && destinations.Count<5) {
				Collider[] treecolliders = Physics.OverlapSphere (sap.position, 1);
				bool notTerrain =false;
				foreach (Collider treeCollider in treecolliders) {
					if (!treeCollider.gameObject.CompareTag ("Ground")) {
						notTerrain =true;
					}
				}
				if (!notTerrain){
				destinations.Add (sap.position);
					sap.planned =true;}

			}
		}
	}
}