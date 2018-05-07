using UnityEngine;
using System.Collections;

public class Attaquer : MonoBehaviour {

	public GlobalVariables gv;

	public void goAttaque(){
		gv.isAttacking = true;
	}

	public void goDefense(){
		if (gv.isEnnemyAttacking == false) {
			gv.isAttacking = false;
			foreach (Unit unit in gv.units) {
				unit.resetPosition ();
			}
			foreach (Unit unit in gv.ennemis) {
				unit.resetPosition ();
			}
		}
	}
}
