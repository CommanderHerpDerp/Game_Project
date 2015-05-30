using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour {
	public class resource{
		public string name;
		public string friendlyName;
		public Texture2D icon;
		public int maxStackSize;
		public bool isSmeltable;
		public bool isFuel;

		public resource(string name,string friendlyName, Texture2D icon,int maxStackSize, bool isSmeltable, bool isFuel){
			this.name = name;
			this.friendlyName = friendlyName;
			this.icon = icon;
			this.maxStackSize = maxStackSize;
			this.isSmeltable = isSmeltable;
			this.isFuel = isFuel;
		}
	}
	public static List<resource> resources = new List<resource>();
	[SerializeField]
	private Texture2D woodIcon=null;



	// Use this for initialization
	void Start () {
		resources.Add(new resource("Wood",woodIcon,64,false,true));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
