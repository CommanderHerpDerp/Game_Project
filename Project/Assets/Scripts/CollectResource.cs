// CollectResource.cs
// CollectResource.cs
using UnityEngine;

public class CollectResource : MonoBehaviour {

	public NavMeshAgent NavMeshAgent;
	public float dist;
	public float minDist = 0.5f;
	private Vector3 startPosition;

	void Awake()
	{
		NavMeshAgent agent = GetComponent ("NavMeshAgent") as NavMeshAgent;
		  
		startPosition = this.transform.position;
	}


	void Start()

		
	{

		// look in the scene for object named ''tree'' and set target destination as its position
		NavMeshAgent agent = this.GetComponent<NavMeshAgent>();
		agent.SetDestination(GameObject.Find("tree").transform.position);
		print ("Found Tree");

	}
	void Update()
		// Check if we've reached the destination
	{
		
		{
			
			NavMeshAgent agent = this.GetComponent<NavMeshAgent>();
			dist = agent.remainingDistance;
			print (dist);

		{
				//if we have arrived, reset the destination back to home
			
			if (dist < 1)

			{
				print ("Here");
				agent.SetDestination (startPosition);
			}


		}
		


		}
	}
}



	





	
	
