using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : MonoBehaviour {
	public string app_name = "s/ nome";
	public int secondary_id = -1;
	public int id = -1;
	
	[HideInInspector] public bool completed = false;

	public void Launch() {
		Debug.Log(app_name + " iniciou");
		this.gameObject.SetActive(true);
	}
}
