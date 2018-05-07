using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Creator : MonoBehaviour {
	
	/* Variables */

	public int typeCreator; // selon la nature du bâtiment
	public int num; // selon le nombre du même type de batiment

	public GlobalVariables gv;
	public int coutbois;
	public int coutfer;
	public int coutnourriture;

	public Quaternion targetRotation;
	public float moveSpeed;
	public float rotateSpeed;
	public bool moving;
	public bool rotating;

	public int health;
	public int currentHealth;
	public int attaque;

	public Vector3 instantiateInit;
	public Vector3 destinationInit;

	public int space;
	public int nbligne;

	public GameObject prefab1;
	public GameObject prefab2;
	public GameObject prefab3;


	public void getRessourcesBack(){
		gv.bois = gv.bois + coutbois;
		gv.fer = gv.fer + coutfer;
		gv.nourriture = gv.nourriture + coutnourriture;
	}

	/* Ajout d'une unité à la file d'attente du bâtiment 
	 * cette fonction est appelé lors du click sur le bouton
	 * "créer ..." du batiment
	 * */

	public void construction(){
		
		if (coutbois <= gv.bois && coutfer <= gv.fer && coutnourriture <= gv.nourriture && gv.population < gv.maxpop) {
			switch (typeCreator) {
			case 1:
				((BaseEpeiste)gv.basesEpeistes[num]).peuple.Add (0);
				break;
			case 2:
				((BaseArcher)gv.basesArchers[num]).peuple.Add (0);
				break;
			case 3:
				((BaseCavalier)gv.basesCavaliers[num]).peuple.Add (0);
				break;
			}
			gv.bois = gv.bois - coutbois;
			gv.fer = gv.fer - coutfer;
			gv.nourriture = gv.nourriture - coutnourriture;
			gv.population++;
		}
	}

	/* Instantiation du GameObject unité*/

	public virtual void create(int side){
		GameObject obj;
		Unit u;
		int nbUnits = gv.archer+gv.epeiste+gv.cavalier;
		int nbEnnemis = gv.archerEnnemy + gv.epeisteEnnemy + gv.cavalierEnnemy;

		switch (typeCreator) {
		case 1:
			obj = Instantiate (prefab1, instantiateInit, Quaternion.identity) as GameObject;
			obj.AddComponent<SoldatEpeiste> ();
			u = obj.GetComponent<SoldatEpeiste> ();
			u.type = 1;
			switch (side) {
			case 0:
				u.ID = nbUnits;
				gv.epeiste = gv.epeiste + 1;
				gv.units.Add (u);
				break;
			case 1:
				u.ID = nbEnnemis;
				gv.ennemis.Add (u);
				gv.epeisteEnnemy = gv.epeisteEnnemy + 1;
				break;
			}
			u.transform.name = "SoldatEpeiste" + u.ID;
			u.gv = gv;
			u.targetRotation = targetRotation;
			u.moveSpeed = moveSpeed;
			u.rotateSpeed = rotateSpeed;
			u.moving = moving;
			u.rotating = rotating;
			u.health = health;
			u.currentHealth = currentHealth;
			u.attaque = attaque;
			u.side = side;

			break;
		case 2:
			obj = Instantiate (prefab2, instantiateInit, Quaternion.identity) as GameObject;
			obj.AddComponent<SoldatArcher> ();
			u = obj.GetComponent<SoldatArcher> ();
			u.type = 2;
			switch (side) {
			case 0:
				u.ID = nbUnits;
				gv.archer = gv.archer + 1;
				gv.units.Add (u);
				break;
			case 1:
				u.ID = nbEnnemis;
				gv.ennemis.Add (u);
				gv.archerEnnemy = gv.archerEnnemy + 1;
				break;
			}
			u.transform.name = "SoldatArcher" + u.ID;
			u.gv = gv;
			u.targetRotation = targetRotation;
			u.moveSpeed = moveSpeed;
			u.rotateSpeed = rotateSpeed;
			u.moving = moving;
			u.rotating = rotating;
			u.health = health;
			u.currentHealth = currentHealth;
			u.attaque = attaque;
			u.side = side;

			break;
		case 3:
			obj = Instantiate (prefab3, instantiateInit, Quaternion.identity) as GameObject;
			obj.AddComponent<SoldatCavalier> ();
			u = obj.GetComponent<SoldatCavalier> ();
			u.type = 3;
			switch (side) {
			case 0:
				u.ID = nbUnits;
				gv.cavalier = gv.cavalier + 1;
				gv.units.Add (u);
				break;
			case 1:
				u.ID = nbEnnemis;
				gv.ennemis.Add (u);
				gv.cavalierEnnemy = gv.cavalierEnnemy + 1;
				break;
			}
			u.transform.name = "SoldatCavalier" + u.ID;
			u.gv = gv;
			u.targetRotation = targetRotation;
			u.moveSpeed = moveSpeed;
			u.rotateSpeed = rotateSpeed;
			u.moving = moving;
			u.rotating = rotating;
			u.health = health;
			u.currentHealth = currentHealth;
			u.attaque = attaque;
			u.side = side;

			break;
		}
			
			
		}
}
