using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryBase : MonoBehaviour {
	[HideInInspector]
	public bool drawInv = false;
	[SerializeField]
	public Dictionary<string,ItemManager.ItemStack> items ;

	protected void AddAllowable(ItemManager.Item item, int maxStackSize){
		ItemManager.ItemStack temp = new ItemManager.ItemStack (item);
		items.Add (item.name,temp);
		items [item.name].maxStackSize = maxStackSize;
	}


	public ItemManager.ItemStack Add(ItemManager.ItemStack stack){
		string key = stack.item.name;
			if (ItemAllowed(stack.item.name)){
				if (items[key].stackSize+stack.stackSize<=items[key].maxStackSize){
					items[key].stackSize +=stack.stackSize;
					stack.stackSize = 0;
				}else{
					stack.stackSize -= items[key].maxStackSize - items[key].stackSize;
					items[key].stackSize = items[key].maxStackSize;
				}
			}
				return stack;
	}

	public ItemManager.ItemStack Remove(string name, int amount){
		if (!items.ContainsKey (name))
			return null;

		ItemManager.ItemStack temp;
		temp = items[name];
		if (items [name].stackSize > amount) {
			temp.stackSize = amount;
			items [name].stackSize -= amount;
		} else
			items [name].stackSize = 0;
		return temp;
		
	}

	public bool ItemAllowed(string name){
		return items.ContainsKey (name);
	}

	// Use this for initialization
	protected virtual void Start () {
		items = new Dictionary<string,ItemManager.ItemStack>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		if (!drawInv)
			return;
		int left = Screen.width - 500;
		int top = 15;
		int width = 485;
		//int height = 450;
		int hPad = 5;
		int vPad = 5;
		int vOffset = 15;
		int hSpacer = 2;
		int vSpacer = 2;
		int height = Mathf.FloorToInt(7 * (width - 2 * hPad + hSpacer) / 9)+vOffset;
		int bWidth = Mathf.FloorToInt((width-2*hPad+hSpacer)/9)-hSpacer;
		int bHeight= Mathf.FloorToInt((height-2*vPad+vSpacer-vOffset)/7)-vSpacer;
		GUI.Box (new Rect (left, top, width, height), "Inventory");
//		for (int a=0; a<7; a++) {
//			for(int b=0; b<9; b++) {
//				int i = b+9*a;
//				int bLeft = left + hPad + b*(bWidth+hSpacer);
//				int bTop = top + vPad + vOffset + a*(bHeight+vSpacer);
//				if (!(items[i]==null)){
//					GUI.Box(new Rect (bLeft, bTop, bWidth ,bHeight ), items[i].item.friendlyName);
//					GUI.Label(new Rect(bLeft+bWidth - 10,bTop+bHeight-20,20,20),items[i].stackSize.ToString());
//				}
//				else
//					GUI.Box(new Rect (bLeft, bTop, bWidth ,bHeight ), "");
//			}
//		}
//		if (GUI.Button (new Rect (20, Screen.height - 50, 100, 30), "Give 62 Wood")) {
//			ItemManager.ItemStack temp = new ItemManager.ItemStack(ItemManager.items["Wood.Oak"]);
//			temp.stackSize=62;
//			int slot;
//			Add (temp,out slot);
//		}
	}

}
