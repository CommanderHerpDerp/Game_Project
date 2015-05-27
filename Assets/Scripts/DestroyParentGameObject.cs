using UnityEngine;
using System.Collections;

public class DestroyParentGameObject : MonoBehaviour {

	public void DestroyObj()
	{
		Destroy(this.gameObject);
	}
}
