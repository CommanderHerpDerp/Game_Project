using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour {
	
	[System.Serializable]
	public class Item{
		public string name;
		public string friendlyName;
		public Texture2D icon;
		public bool isSmeltable;
		public bool isFuel;

		public Item(string name,string friendlyName, Texture2D icon, bool isSmeltable, bool isFuel){
			this.name = name;
			this.friendlyName = friendlyName;
			this.icon = icon;
			this.isSmeltable = isSmeltable;
			this.isFuel = isFuel;
		}
	}
	[System.Serializable]
	public class ItemStack{
		public Item item;
		public int stackSize;
		public int maxStackSize;
		public ItemStack(Item item){
			this.item = item;
			this.stackSize = 0;
			this.maxStackSize = 1;
		}

		public ItemStack add(ItemStack item){
			if (item.item == this.item){
				if (stackSize + item.stackSize <= maxStackSize||maxStackSize==-1) {
					stackSize += item.stackSize;
					item.stackSize = 0;
				} else {
					int overflow = stackSize + item.stackSize - maxStackSize;
					stackSize = maxStackSize;
					item.stackSize = overflow;
				}
			}
				return item;
		}
	}
	public static Dictionary<string,Item> items = new Dictionary<string,Item>();

	private void ItemsAdd(string nameid, string friendlyName, Texture2D icon, bool isSmeltable, bool isFuel){
		items.Add (nameid,new Item(nameid,friendlyName,icon,isSmeltable,isFuel));
	}




	[SerializeField]
	private Texture2D woodIcon=null;
	[SerializeField]
	private Texture2D stoneIcon=null;




	// Use this for initialization
	void Start () {
		ItemsAdd("resource.wood","Wood",woodIcon,false,true);
		ItemsAdd("resource.stone","Stone",stoneIcon,false,true);


		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
