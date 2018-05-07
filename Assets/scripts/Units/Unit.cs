using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public abstract class Unit : MonoBehaviour {

	/*Variables */
	public int type; 

	public Quaternion targetRotation;
	public float moveSpeed;
	public float rotateSpeed;
	public bool moving;
	public bool rotating;
	public int health;
	public int currentHealth;
	public int attaque;
	public int side;

	public GlobalVariables gv;
	public Image panel;
	public Text stats;

	public int ID;

	public Vector3 destinationInit;
	public Vector3 destination;

	public float distanceEnnemy;
	public Vector3 closestEnnemyPosition;
	public Unit unitEnnemy;

	public bool attacking;
	public bool attackingBuildings;

	public float nextTimeAction = 0f;
	public float period = 0.5f;

	/* On change la couleur de l'unité si c'est un ennemi (rouge) */
	/* L'unité commence par se diriger vers son point de raliement */

	public virtual void Start () {
		switch (side) {
		case 0:
			break;
		case 1:
			Renderer rend1 = gameObject.GetComponent<Renderer> ();
			rend1.material.SetColor ("_Color", Color.red);
			break;
		}
		rotateSpeed = 100;
		targetRotation = Quaternion.identity;
		setDestinationInit ();
		StartMove (destinationInit);
	}


	/*-	Vérification de la vie de l’unité. Destruction de l’unité si santé est en dessous de 0.
-	Modifications des ID des unités. Nécessaire au positionnement des troupes dans la base.
-	Vérification de l’état d’attaque de l’unité ou non selon si le joueur ou l’ennemi attaque.
-	Assignation de la cible ennemie la plus proche.
-	Ne bouge plus lorsqu’il est proche de se cible et qu’il attaque.
-	S’il attaque : il attaque sa cible s’il en a une et qu’il est assez proche,
	s’il est trop loin, il se déplace dans sa direction,
	sinon il attaque les bâtiments en finissant par le bâtiment principal.
-	S’il se tourne, il continue jusqu’à qu’il soit dans la bonne direction.
-	S’il se déplace, il continue jusqu’à sa destination.
		*/

	public virtual void Update () {



		isDead ();
		resetID ();

		if (!gv.isAttacking && !gv.isEnnemyAttacking) {
			attacking = false;
		}

		if (!attacking && gv.time > nextTimeAction ) {
			nextTimeAction += period;
			setTarget ();
		} 

		if ((gv.isAttacking || gv.isEnnemyAttacking) && !attacking) {
			unitAttaque ();
		}
			


		if (attacking && distanceEnnemy < 5f) {
			moving = false;
		}

		if (attacking && gv.time > nextTimeAction) {
			nextTimeAction += period;
			unitAttaque ();

			if (attackingBuildings) {

				attackBuildings ();
			} else {
				if (distanceEnnemy < 20f) {
					makeAttack ();
				} else {
					setTarget ();
					StartMove (closestEnnemyPosition);
				}
			}
		}
			

		if (rotating){
			TurnToTarget ();
		}
		else if (moving) {
			MakeMove ();
		}

	}

	/* l'Unit se tourne vers la direction dans laquelle is veut aller */

	private void TurnToTarget() {

		transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed);
		Quaternion inverseTargetRotation = new Quaternion(-targetRotation.x, -targetRotation.y, -targetRotation.z, -targetRotation.w);
		if(transform.rotation == targetRotation || transform.rotation == inverseTargetRotation) {
			rotating = false;
			moving = true;
		}
	}

	/* l'Unit se déplace vers la direction dans laquelle il veut aller */

	private void MakeMove() {
		transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * moveSpeed);
		if(transform.position == destination) moving = false;
	}

	/* l'Unit initialise son mouvement */

	public void StartMove(Vector3 destination) {
		this.destination = destination;
		targetRotation = Quaternion.LookRotation (destination - transform.position);
		rotating = true;
		moving = false;
	}

	public virtual void combat(Unit u){

	}

	/* Initialisation du mouvement d'attaque */

	public void unitAttaque(){
		int nbEnnemy;
		if (side == 0) {
			nbEnnemy = gv.archerEnnemy + gv.epeisteEnnemy + gv.cavalierEnnemy;
		} else {
			nbEnnemy = gv.archer + gv.epeiste + gv.cavalier;
		}
		attacking = true;

			if (nbEnnemy == 0) {
			distanceEnnemy = 10000000;
				attackingBuildings = true;
			} else {
			attackingBuildings = false;
			}
	}

	/* Porte le coup à l'ennemi, ou va voir ailleurs si l'ennemi est mort */

	public void makeAttack(){
		if (unitEnnemy.currentHealth > 0) {
			combat (unitEnnemy);
		} 
			else {
			setTarget ();
			unitAttaque ();
			}
		}

	/* Cherche l'ennemi le plus proche */

	public void setTarget(){
		distanceEnnemy = 10000000;
		if (side == 0) {
			foreach (Unit unit in gv.ennemis) {
				float distance = Vector3.Distance (transform.position, unit.transform.position);
				if (distance < distanceEnnemy) {
					distanceEnnemy = distance;
					closestEnnemyPosition = unit.transform.position;
					unitEnnemy = unit;
				}
			}
		} else {
			foreach (Unit unit in gv.units) {
				float distance = Vector3.Distance (transform.position, unit.transform.position);
				if (distance < distanceEnnemy) {
					distanceEnnemy = distance;
					closestEnnemyPosition = unit.transform.position;
					unitEnnemy = unit;
				}
			}
		}

	}

	/* Fonction de formation de la position des unités dans les rangs */
	/* Petit Calcul selon l'espace entre chaque unité et le nombre d'unité par ligne */

	public void setDestinationInit(){
		Vector3 dest;
		int space = gv.space;
		int nbLigne = gv.nbLigne;
		int rang = ID / nbLigne;
		if (side == 0) {
			dest = gv.destinationInit;
		} else {
			dest = gv.destinationEnnemisInit;
		}
			if (ID % 2 == 0) {
				destinationInit.x = dest.x + space * ID / 2 - nbLigne / 2 * space * rang;
			} else {
				destinationInit.x = dest.x - space * (ID + 1) / 2 + nbLigne / 2 * space * rang;
			}
			destinationInit.z = dest.z - space * rang;

	}

	/* Retour dans les rangs */

	public void resetPosition(){
		int nbEnnemis = gv.archerEnnemy + gv.epeisteEnnemy + gv.cavalierEnnemy;
		if (!gv.isEnnemyAttacking || nbEnnemis == 0) {
			attacking = false;
			StartMove (destinationInit);
		}
	}

	/* On réattribut, utile lors de la création ou destruction d'unit
	 * pour la formation dans les rangs */

	public void resetID(){
		foreach (Unit unit in gv.units) {
			unit.ID = gv.units.IndexOf (unit);
		}
		foreach (Unit unit in gv.ennemis) {
			unit.ID = gv.ennemis.IndexOf (unit);
		}

		setDestinationInit ();
	}

	/* Regarde si l'Unit n'a plus de vie, et la détruit si c'est le cas */

	public void isDead(){
		if (currentHealth <= 0) {
			switch (side) {
			case 0:
				gv.population--;
				gv.units.Remove (this);
				switch (type) {
				case 1:
					gv.epeiste--;
					break;
				case 2:
					gv.archer--;
					break;
				case 3:
					gv.cavalier--;
					break;	
				}
				break;
			case 1:
				gv.ennemis.Remove (this);
				switch (type) {
				case 1:
					gv.epeisteEnnemy--;
					break;
				case 2:
					gv.archerEnnemy--;
					break;
				case 3:
					gv.cavalierEnnemy--;
					break;	
				}
				break;
			}

			Destroy (this.gameObject);
		}
	}

	/*les unités regardent quel bâtiment encore en vie il reste,
	 * et vont l'attaquer lorsqu'ils se trouvent assez proches */

	public void attackBuildings(){
		float distance;
		Vector3 buildingPosition;
		int i = 0;
		if (side == 0) {
			while (((Base)gv.basesEnnemy [i]).currentHealth <= 0 && i < gv.basesEnnemy.Count) {
				i++;
			}
			buildingPosition = ((Base)gv.basesEnnemy [i]).transform.position;
			distance = Vector3.Distance (transform.position, buildingPosition);
			StartMove (buildingPosition);
			if (distance < 20f) {
				((Base)gv.basesEnnemy [i]).currentHealth -= attaque;
			}
		}
		else {
			while (((Base)gv.bases [i]).currentHealth <= 0 && i < gv.bases.Count) {
				i++;
			}
			buildingPosition = ((Base)gv.bases [i]).transform.position;
			distance = Vector3.Distance (transform.position, buildingPosition);
			StartMove (((Base)gv.bases [i]).transform.position);
			if (distance < 20f) {
				((Base)gv.bases [i]).currentHealth -= attaque;
			}
		}
	}
		
}
