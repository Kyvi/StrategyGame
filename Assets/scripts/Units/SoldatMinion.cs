using UnityEngine;
using System.Collections;

public class SoldatMinion : MonoBehaviour {

	public float dureeVie;
	public GlobalVariables gv;
	public int IDminion;
	public int metier;

	public float nextActionTime ;
	private static float period = 1f; 
	// Use this for initialization
	void Start () {
	}
	
	/* On retire le minion du jeu si sa durée de vie devient négative */
	void Update () {	
		if (gv.time > nextActionTime ) {
			nextActionTime += period;
			dureeVie = dureeVie - period;
			if (dureeVie <= 0) {
				gv.minion--;
				gv.population--;
				gv.minions.Remove(this);
				switch (metier) {
				case 0:
					gv.minionDisponible--;
					break;
				case 1:
					gv.minionbois--;
					break;
				case 2:
					gv.minionfer--;
					break;
				case 3:
					gv.minionnourriture--;
					break;
				}

				Destroy (this.gameObject);

			}
		}
	}
}
