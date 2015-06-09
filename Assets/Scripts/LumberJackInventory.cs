using UnityEngine;
using System.Collections;

public class LumberJackInventory : InventoryBase {

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		AddAllowable (ItemManager.items ["resource.wood"], 1);
	}
}
