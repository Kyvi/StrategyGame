using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {

	/**** Variables ****/

	public GlobalVariables gv;
	private float nextActionTime = 0.0f;
	private float period = 5f;
	private float vague = 121.1f;
	private float vaguePrec = 0f;

	public GameObject soldatArcher;
	public GameObject soldatEpeiste;
	public GameObject soldatCavalier;

	// flag permet de ne pas avoir à que les ennemis battent constamment en retraite
	private bool flag = true; 
	private Creator creator;


	// On attache le script au gameObject
	void Start () {
		creator = gameObject.GetComponent<Creator> ();
	}

	/* crée des unités régulièrement jusqu’à ce qu’on arrive
		 	à la moitié du temps restant de la prochaine Vague.
			Lors de la vague, envoie les unités créées à l’attaque.
		Le temps de la prochaine vague est modifié, de même que le temps de
		production de chaque unité. Le type d’unité créé est modifié également.
		Si moins de 5 unités restantes, bat en retraite. ***/
	
	void Update () {


		if (gv.time > nextActionTime && gv.time < (int)(vaguePrec + vague) / 2) {
			nextActionTime += period;
			creator.create (1);
		} else {
			if (gv.time > nextActionTime && gv.time < vague - period) {
				nextActionTime += period;
			}
		}

		if (gv.time > nextActionTime && gv.time > vague && gv.time < vague+0.1) {
			nextActionTime += period;
			vaguePrec = vague;
			vague += Mathf.Max (10, vague - 15);
			period = Mathf.Max (period - 1, 1);
			if (creator.typeCreator == 3) {
				creator.typeCreator = 1;
			} else {
				creator.typeCreator++;
			}
			sendUnits ();
		}



		if (gv.ennemis.Count <= 5  && !flag) {
			retreat ();
			flag = true;
		}


	}

	public void sendUnits(){
		
		gv.isEnnemyAttacking = true;
		flag = false;
	}

	public void retreat(){
			gv.isEnnemyAttacking = false;

	}

} 