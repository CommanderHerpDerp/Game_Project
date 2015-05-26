using UnityEngine;
using System.Collections;

public class MousePoint : MonoBehaviour
{

	RaycastHit hit;


	public Transform Target;

	private Vector3 mouseDownPoint;
	 

	void awake()
	{
		mouseDownPoint = Vector3.zero;
	}

	void Update ()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {

			//Store point at mouse button down
			if (Input.GetMouseButtonDown (0))
				mouseDownPoint = hit.point;



		 {

				if (Input.GetMouseButtonDown (0)) {
					GameObject targetObj = Instantiate (Target, hit.point, Quaternion.identity) as GameObject;
					targetObj.name = "Target";
				}
			}
		}
	}



}

