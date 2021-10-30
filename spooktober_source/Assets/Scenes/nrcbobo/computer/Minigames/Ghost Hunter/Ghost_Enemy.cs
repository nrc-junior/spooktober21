using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum Status {
	ok,
	perigo,
	morrendo,
	amigo
}

public class Ghost_Enemy : MonoBehaviour {
	public GameObject actor;
	public TextMeshProUGUI chance_to_flee;

	public Status status = Status.ok;
	public int life;
	public int armor;
	int average;
	int bad;
	int start_life;

	public Ghost_Enemy(int life, int armor, GameObject actor, TextMeshProUGUI txt) {
		this.life = life;
		this.armor = armor;
		this.actor = actor;
		chance_to_flee = txt;
		STATUSMATH();
	}

	void STATUSMATH() {
		average = Mathf.CeilToInt(life / 2);
		bad = Mathf.CeilToInt(average / 2);
		start_life = life;
	}

	public bool dealDamage(int damage) {
		life = life + armor - damage;
		setStatus();
		return life <= 0;
	}

	public bool heal() {
		life++;
		setStatus();
		return status == Status.amigo;
	}

	void setStatus() {
		if (life >= start_life*2) {
			print("amigo");
			status = Status.amigo;
			return;
		}

		if (life >= average) {
			print("ok");

			status = Status.ok;
			return;
		}

		if (life < average && life > bad){ 	
			print("perigo");
			status = Status.perigo;
			return;
		}

		if (life <= bad) {
			print("morrendo");
			status = Status.morrendo;
		}
		
	}

	public Color getStatus() {
		chance_to_flee.text = $"Chance to Flee: " +
		                      decimal.Round((life * 100) / (start_life * 2), 2, MidpointRounding.AwayFromZero) + $"%";
		Color c = Color.green;
		switch (status) {
			case Status.amigo:
				c = Color.cyan;
				break;
			case Status.ok:
				c = Color.green;
				break;
			case Status.perigo:
				c = Color.yellow;
				break;
			case Status.morrendo:
				c = Color.red;
				break;
		}
		return c;
	}
}
