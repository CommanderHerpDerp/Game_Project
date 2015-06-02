using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour {
	public class item{
		public string name;
		public string friendlyName;
		public Texture2D icon;
		public int maxStackSize;
		public bool isSmeltable;
		public bool isFuel;

		public item(string name,string friendlyName, Texture2D icon,int maxStackSize, bool isSmeltable, bool isFuel){
			this.name = name;
			this.friendlyName = friendlyName;
			this.icon = icon;
			this.maxStackSize = maxStackSize;
			this.isSmeltable = isSmeltable;
			this.isFuel = isFuel;
		}
	}

	
	public static Dictionary<string,item> items = new Dictionary<string,item>();

	private void ItemsAdd(string nameid, string friendlyName, Texture2D icon, int maxStackSize, bool isSmeltable, bool isFuel){
		items.Add (nameid,new item(nameid,friendlyName,icon,maxStackSize,isSmeltable,isFuel));
	}

	public item GetItemByKey(string key){
		return items[key];
	}



	[SerializeField]
	private Texture2D woodIcon=null;



	// Use this for initialization
	void Start () {
		ItemsAdd("Wood","Wood",woodIcon,64,false,true);
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
