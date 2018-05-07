using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeFer : MonoBehaviour {

	public Text input;
	public GlobalVariables gv;

	/*On récupère les données qu’on transpose en int.
	On regarde si cette donnée est valide,
	(c’est-à-dire si le nombre assigné souhaité est inférieur
	au nombre de minions disponibles ajouté à ceux déjà assignés à cette ressource).
	Si c’est le cas : Le nombre de minion disponible devient égal
	au nombre de minions disponibles ajouté à ceux déjà assignés auquel on retire
	le nombre assigné souhaité.
	On change la variable des assignations de ressource de chaque minion à l’aide d’une boucle. */

	public void change(){
		int intInput = int.Parse (input.text);
		if ( intInput <= gv.minionDisponible + gv.minionfer) {
			gv.minionDisponible = gv.minionDisponible + gv.minionfer - intInput;
			gv.minionfer = intInput;

			foreach (SoldatMinion sm in (gv.minions)) {
				if (sm.metier == 2) {
					sm.metier = 0;
				}
			}

			int compteur = intInput;
			int k = 0;
			while (compteur > 0 && k<gv.minions.Count) {
				if ( ((SoldatMinion)gv.minions[k]).metier == 0 ){
					((SoldatMinion)gv.minions[k]).metier = 2;
					compteur--;
				}
				k++;
			}

		}
	}
}