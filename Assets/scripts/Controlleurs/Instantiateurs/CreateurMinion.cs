using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateurMinion : MonoBehaviour {

	/* Variables */
	public GlobalVariables gv;
	public int coutbois;
	public int coutfer;
	public int coutnourriture;

	public float dureeVie;

	public GameObject prefab;

	/* Récupérer les ressources */
	public void getRessourcesBack(){
		gv.bois = gv.bois + coutbois;
		gv.fer = gv.fer + coutfer;
		gv.nourriture = gv.nourriture + coutnourriture;
	}

	/* Ajout d'un minion à la file d'attente du bâtiment principal
	 * cette fonction est appelé lors du click sur le bouton
	 * "créer minion" de la base principale alliée
	 * */

	public void constructionMinion(){
		if (coutbois <= gv.bois && coutfer <= gv.fer && coutnourriture <= gv.nourriture && gv.population < gv.maxpop) {
			((BaseAliee)gv.bases [gv.bases.Count-gv.age]).peuple.Add (0);
			gv.bois = gv.bois - coutbois;
			gv.fer = gv.fer - coutfer;
			gv.nourriture = gv.nourriture - coutnourriture;
		}
	}

	/* Instantiation du GameObject minion*/

	public void CreateMinion(){
		GameObject obj = Instantiate (prefab) as GameObject;
		obj.AddComponent<SoldatMinion> ();
		SoldatMinion m = obj.GetComponent<SoldatMinion> ();
		m.nextActionTime = Time.time;
		m.dureeVie = dureeVie;
		m.gv = gv;
		m.metier = 0;
		m.IDminion = gv.minion;
		gv.population = gv.population + 1;
		gv.minion = gv.minion + 1;
		gv.minionDisponible = gv.minionDisponible + 1;
		gv.minions.Add (m);
	}



}
