using UnityEngine;
using System.Collections;

public class DestroyParentGameObject : MonoBehaviour {

	void Start()
	{
		Destroy(this.gameObject.transform.parent.gameObject);
	}
}
