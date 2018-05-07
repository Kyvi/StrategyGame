using UnityEngine;
using System.Collections;

public class BuildBase: MonoBehaviour {

	public GlobalVariables gv;

	/* Construction des 3 bases à des endroits prédéterminés selon
	 * le nombre de bâtiments déjà présents si on a les ressources nécessaire.
	 * Le coût des bâtiments est modifié
	 * Le niveau d'évolution augmente */
	 
	public void buildBaseArcher(){
		if (gv.bois >= gv.coutBoisBase && gv.fer >= gv.coutFerBase && gv.nourriture >= gv.coutNourritureBase && gv.age <= 3) {
			gv.createBaseArcher (0, new Vector3 (80+80*gv.age, 0, -420));
			setCout ();
			gv.age++;
		}

	}

	public void buildBaseEpeiste(){
		if (gv.bois >= gv.coutBoisBase && gv.fer >= gv.coutFerBase && gv.nourriture >= gv.coutNourritureBase && gv.age <= 3) {
			gv.createBaseEpeiste (0, new Vector3 (80+80*gv.age, 0, -420));
			setCout ();
			gv.age++;
		}

	}

	public void buildBaseCavalier(){
		if (gv.bois >= gv.coutBoisBase && gv.fer >= gv.coutFerBase && gv.nourriture >= gv.coutNourritureBase && gv.age <= 3) {
			gv.createBaseCavalier (0, new Vector3 (80+80*gv.age, 0, -420));
			setCout ();
			gv.age++;
		}

	}

	/* Le prochain seuil = ancien seuil + delta en
	fonction du niveau */

	public void setCout(){
		gv.coutBoisBase += (int) (gv.coutBoisBase * gv.time/100) ;
		gv.coutFerBase += (int) (gv.coutFerBase* gv.time/100);
		gv.coutNourritureBase += (int)(gv.coutNourritureBase * gv.time / 100);
	}
}
