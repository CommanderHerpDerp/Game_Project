using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public int inventorySlots = 63;
	[HideInInspector]
	public bool drawInv = false;
	[SerializeField]
	public ItemManager.ItemStack[] slots;
	public ItemManager.ItemStack AddTo(int slot, ItemManager.ItemStack itemStack){
		if (slots [slot] == null | slots [slot].item == itemStack.item) {
			int overflow = slots [slot].add(itemStack.stackSize);
			if (overflow == 0)
				return null;
			else{
				itemStack.stackSize = overflow;
				return itemStack;
			}
		} else {
			return itemStack;
		}
	}
	public ItemManager.ItemStack Add(ItemManager.ItemStack itemStack, out int slot){
		slot = -1;
		for(int i=0;i<inventorySlots;i++){
			if (slots[i]==null){
				slots[i]=itemStack;
				slot = i;
				return null;
			}
			else if (slots[i].item == itemStack.item && slots[i].stackSize< slots[i].maxStackSize){
				if (slots[i].stackSize+itemStack.stackSize<=itemStack.maxStackSize){
					slots[i].stackSize +=itemStack.stackSize;
					slot = i;
					return null;
				}else{
					itemStack.stackSize -= itemStack.maxStackSize - slots[i].stackSize;
					slots[i].stackSize = itemStack.maxStackSize;
					slot = i;
				}
			}
		}
		return itemStack;
	}

	public ItemManager.ItemStack Remove(int slot){
		ItemManager.ItemStack temp;
		temp = slots [slot];
		slots [slot] = null;
		return temp;
	}
	// Use this for initialization
	void Start () {
		slots = new ItemManager.ItemStack[inventorySlots];
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
		GUI.Box (new Rect (left, top, width, height), "Inventory");
		for (int a=0; a<7; a++) {
			for(int b=0; b<9; b++) {
				int bWidth = Mathf.FloorToInt((width-2*hPad+hSpacer)/9)-hSpacer;
				int bHeight= Mathf.FloorToInt((height-2*vPad+vSpacer-vOffset)/7)-vSpacer;
				int bLeft = left + hPad + b*(bWidth+hSpacer);
				int bTop = top + vPad + vOffset + a*(bHeight+vSpacer);
				GUI.Box(new Rect (bLeft, bTop, bWidth ,bHeight ), "Wood");
				GUI.Label(new Rect(bLeft+bWidth - 20,bTop+bHeight-20,20,20),"0");
			}
		}
	}

}
