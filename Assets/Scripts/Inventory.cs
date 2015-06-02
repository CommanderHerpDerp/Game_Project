using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public Dictionary<int,ItemManager.item> items = new Dictionary<int, ItemManager.item>();
	public int Add(ItemManager.item item){
		items.Add (item);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
