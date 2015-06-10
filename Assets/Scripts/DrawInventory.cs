using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawInventory : MonoBehaviour {
	public List<GameObject> icons;

	public void drawList(List<ItemManager.ItemStack> list){
		for (int a=0; a<list.Count-1; a++) {
			if (a<icons.Count-1){

			}
			else{
			}
		}
	}

	void Start(){
		icons = new List<GameObject>();
	}


}
