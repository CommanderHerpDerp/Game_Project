using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour {
	
	[System.Serializable]
	public class Item{
		public string name;
		public string friendlyName;
		public Texture2D icon;
		public int maxStackSize;
		public bool isSmeltable;
		public bool isFuel;

		public Item(string name,string friendlyName, Texture2D icon,int maxStackSize, bool isSmeltable, bool isFuel){
			this.name = name;
			this.friendlyName = friendlyName;
			this.icon = icon;
			this.maxStackSize = maxStackSize;
			this.isSmeltable = isSmeltable;
			this.isFuel = isFuel;
		}
	}
	[System.Serializable]
	public class ItemStack{
		public Item item;
		public int stackSize;
		public int maxStackSize{
			get; private set;
		}
		public ItemStack(Item item){
			this.item = item;
			this.stackSize = 1;
			this.maxStackSize = item.maxStackSize;
		}

		public int add(int amount){
			if (stackSize + amount <= maxStackSize) {
				stackSize += amount;
				return 0;
			} else {
				int overflow = stackSize + amount - maxStackSize;
				stackSize = maxStackSize;
				return overflow;
			}
		}
	}
	public static Dictionary<string,Item> items = new Dictionary<string,Item>();

	private void ItemsAdd(string nameid, string friendlyName, Texture2D icon, int maxStackSize, bool isSmeltable, bool isFuel){
		items.Add (nameid,new Item(nameid,friendlyName,icon,maxStackSize,isSmeltable,isFuel));
	}

	public Item GetItemByKey(string key){
		return items[key];
	}



	[SerializeField]
	private Texture2D woodIcon=null;



	// Use this for initialization
	void Start () {
		ItemsAdd("Wood.Oak","Oak Wood",woodIcon,64,false,true);
		ItemStack test = new ItemStack (items ["Wood.Oak"]);

		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
