using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SoldatCavalier : Unit {




	public override void Start () {
		base.Start ();
	}

	// Update is called once per frame
	public override void Update () {
		base.Update();
	}
		
	// Assignation des blessures à l'Unit u

	public override void combat(Unit u){
		base.combat (u);
		switch (u.type) {
		case 1:
			u.currentHealth = u.currentHealth - (attaque*2);
			break;
		case 2:
			u.currentHealth = u.currentHealth - attaque / 2;
			break;
		case 3:
			u.currentHealth = u.currentHealth - attaque;
			break;
		}
	}

}
