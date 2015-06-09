using UnityEngine;
using System.Collections;

public class WoodCutterInventory : InventoryBase {
	
	// Use this for initialization
	protected override void Start () {
		base.Start ();
		AddAllowable (ItemManager.items ["resource.wood"], 8);
	}
}
