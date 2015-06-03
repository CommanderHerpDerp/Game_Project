using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public int inventorySlots = 63;
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
}
