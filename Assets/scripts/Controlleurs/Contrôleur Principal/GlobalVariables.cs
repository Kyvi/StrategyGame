using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SceneManagement;


public class GlobalVariables : MonoBehaviour {

	/* Informations bases */

	public ArrayList bases = new ArrayList();
	public ArrayList basesEnnemy = new ArrayList();
	public ArrayList basesEpeistes =  new ArrayList();
	public ArrayList basesArchers =  new ArrayList();
	public ArrayList basesCavaliers =  new ArrayList();

	public BaseAliee ba;
	public BaseAliee be;

	/* UI Elements */

	public GameObject Chargements;
	public GameObject barreChargement;
	public Text texteChargement;
	public Text texteVie;

	public Image activepanel;
	public GameObject activeBarre;
	public Text activeText;
	public Text activeTextVie;
	public GameObject activeBase;


	public Image pausePanel;
	public Image panelVictoire;
	public Image panelDefaite;

	/* HUDTop */
	public Text general;
	public Text armee;
	public Text ressources;
	public Text gestion;

	/* Prefabs */

	public GameObject prefabBaseEpeiste;
	public GameObject prefabBaseArcher;
	public GameObject prefabBaseCavalier;

	public GameObject prefabSoldatEpeiste;
	public GameObject prefabSoldatArcher;
	public GameObject prefabSoldatCavalier;

	/* Variables unités */

	public Quaternion targetRotation;
	public float moveSpeed;
	public float rotateSpeed;
	public bool moving;
	public bool rotating;
	public int space;
	public int nbLigne;
	public Vector3 destinationInit;
	public Vector3 destinationEnnemisInit;

	/* phases d'attaques */
	public bool isAttacking;
	public bool isEnnemyAttacking;


	/* Variables Bases */

	public float timerMax;
	public float timer;
	public float periodChargement = 0.1f;

	public float timerValideMax;
	public float timerValide;
	public bool isValid;

	/* Stats Epeiste */
	public Image panelBaseEpeiste;
	public Image panelEnnemyEpeiste;

	public int coutBoisEpeiste;
	public int coutFerEpeiste;
	public int coutNourritureEpeiste;

	public int healthSoldatEpeiste;
	public int currentHealthSoldatEpeiste;
	public int attaqueSoldatEpeiste;

	public int typeCreatorEpeiste;

	public int healthBaseEpeiste;
	public int currentHealthBaseEpeiste;

	public int typeEpeiste;
	public String nameEpeiste;

	/* Stats Archer */
	public Image panelBaseArcher;
	public Image panelEnnemyArcher;

	public int coutBoisArcher;
	public int coutFerArcher;
	public int coutNourritureArcher;

	public int healthSoldatArcher;
	public int currentHealthSoldatArcher;
	public int attaqueSoldatArcher;

	public int typeCreatorArcher;

	public int healthBaseArcher;
	public int currentHealthBaseArcher;

	public int typeArcher;
	public String nameArcher;

	/* Stats Cavalier */
	public Image panelBaseCavalier;
	public Image panelEnnemyCavalier;

	public int coutBoisCavalier;
	public int coutFerCavalier;
	public int coutNourritureCavalier;

	public int healthSoldatCavalier;
	public int currentHealthSoldatCavalier;
	public int attaqueSoldatCavalier;

	public int typeCreatorCavalier;

	public int healthBaseCavalier;
	public int currentHealthBaseCavalier;

	public int typeCavalier;
	public String nameCavalier;

	/* Population*/
	public int minion;
	public int minionDisponible;
	public ArrayList minions = new ArrayList();

	public int epeiste;
	public int archer;
	public int cavalier;
	public int population;
	public int maxpop;
	public ArrayList units = new ArrayList();

	public int epeisteEnnemy;
	public int archerEnnemy;
	public int cavalierEnnemy;
	public ArrayList ennemis = new ArrayList();


	/* Gestion des ressources */
	public int bois;
	public int minionbois;
	public int fer;
	public int minionfer;
	public int nourriture;
	public int minionnourriture;

	/* Information boutons construction de base */
	public int coutBoisBase;
	public int coutFerBase;
	public int coutNourritureBase;
	public int age;
	public Button buildButtonArcher;
	public Button buildButtonEpeiste;
	public Button buildButtonCavalier;
	public Text buildArcher;
	public Text buildEpeiste;
	public Text buildCavalier;



	/* Variables temporelles */
	private static float nextActionTime;
	private static float period = 1f;
	public float time;

	/* Création des bâtiments initiaux */

	void Start(){
		nextActionTime = 0f;


		createBaseArcher (0, new Vector3 (80, 0, -420));
		createBaseEpeiste(0, new Vector3(0,0,-420));
		createBaseCavalier (0, new Vector3 (-80, 0, -420));

		createBaseArcher(1, new Vector3(80,0,-120));
		createBaseEpeiste(1, new Vector3(0,0,-120));
		createBaseCavalier(1, new Vector3(-80,0,-120));
		bases.Add (ba);
		basesEnnemy.Add (be);
	}
		
	/* Incrémentation des ressources, vérification du nombre de bâtiments disponibles,
	 * Texte Hud et Boutons de constructions de bâtiments */

	void Update(){

		time = Time.timeSinceLevelLoad;

		if (time > nextActionTime ) {
			nextActionTime += period;
			bois = bois + minionbois;
			fer = fer + minionfer;
			nourriture = nourriture + 2*minionnourriture;
		}

		if (age == 4) {
			buildButtonArcher.interactable = false;
			buildButtonEpeiste.interactable = false;
			buildButtonCavalier.interactable = false;
		}



		int armeeTot = archer + epeiste + cavalier;
		int ennemyTot = archerEnnemy + epeisteEnnemy + cavalierEnnemy;

		general.text = "Population : " + population.ToString () + "/" + maxpop.ToString () + "\n" +
		"Minions : " + minion.ToString () + "\n" +
		"Armée : " + armeeTot.ToString () + "\n" +
		"Ennemis : " + ennemyTot.ToString ();

		armee.text = "Archers : " + archer.ToString() + "\n" +
			"Cavaliers : " + cavalier.ToString() + "\n" +
			"Epeistes : " + epeiste.ToString();

		ressources.text = " Bois : " + bois.ToString() + "\n" +
			" Fer : " + fer.ToString() + "\n" +
			" Nourriture : " + nourriture.ToString();

		gestion.text = " Minions Disponibles : " + minionDisponible.ToString () + "\n" +
		" Minions Bois : " + minionbois.ToString () + "\n" +
		" Minions Fer : " + minionfer.ToString () + "\n" +
		" Minions Nourriture : " + minionnourriture.ToString ();


		buildArcher.text = "Construire Base Archer : \n" +
		setBuildTextCost ();

		buildEpeiste.text = "Construire Base Epeiste : \n" +
			setBuildTextCost ();

		buildCavalier.text = "Construire Base Cavalier : \n" +
			setBuildTextCost ();
			

	}

	public string setBuildTextCost(){
		if (age == 4) {
			return " Niveau Max Atteint ! ";
		}
		else {
			return "Coût Bois : " + coutBoisBase.ToString () + "\n" +
			"Coût Fer : " + coutFerBase.ToString () + "\n" +
			"Coût Nourriture : " + coutNourritureBase.ToString () + "\n";
		}
	}

	/* Méthodes de création des 3 bâtiments 
	Instantiation du GameObject et de ses 2 scripts : Creator et Base... 
	Chaque variable est assignée à l'aide des variables de ce contrôleur */

	public void createBaseArcher(int side, Vector3 position){

		GameObject obj;
		BaseArcher baseArcher;
		Creator creator;
		obj = Instantiate (prefabBaseArcher, position, Quaternion.identity) as GameObject;
		obj.AddComponent<Creator> ();
		obj.AddComponent<BaseArcher> ();
		baseArcher = obj.GetComponent<BaseArcher> ();
		creator = obj.GetComponent<Creator> ();
		creator.typeCreator = typeCreatorArcher;
		creator.num = basesArchers.Count;

		creator.gv = this;
		creator.coutbois = coutBoisArcher;
		creator.coutfer = coutFerArcher;
		creator.coutnourriture = coutNourritureArcher;

		creator.targetRotation = targetRotation;
		creator.moveSpeed = moveSpeed;
		creator.rotateSpeed = rotateSpeed;
		creator.moving = moving;
		creator.rotating = rotating;


		creator.health = healthSoldatArcher;
		creator.currentHealth = currentHealthSoldatArcher;
		creator.attaque = attaqueSoldatArcher;

		creator.instantiateInit = position;
		creator.destinationInit = destinationInit;

		creator.space = space;
		creator.nbligne = nbLigne;

		creator.prefab1 = prefabSoldatEpeiste;
		creator.prefab2 = prefabSoldatArcher;
		creator.prefab3 = prefabSoldatCavalier;


		if (side == 0) {
			baseArcher.panel = panelBaseArcher;
		} else{
			baseArcher.panel = panelEnnemyArcher;
		}

		baseArcher.gv = this;
		baseArcher.health = healthBaseArcher;
		baseArcher.currentHealth = currentHealthBaseArcher;
		baseArcher.side = side;

		baseArcher.timerMax = timerMax;
		baseArcher.timer = timer;
		baseArcher.isValid = isValid;
		baseArcher.timerValide = timerValide;
		baseArcher.timerValideMax = timerValideMax;

		if (side == 0){
		GameObject obj1;
		obj1 = Instantiate (barreChargement) as GameObject;
		obj1.transform.parent = Chargements.transform;
		obj1.transform.name = "Barre File Attente Archer";
		baseArcher.barreChargement = obj1;
		Text obj2;
		obj2 = Instantiate (texteChargement) as Text;
		obj2.transform.name = "Texte File Attente Archer";
		baseArcher.texteChargement = obj2;
		obj2.transform.parent = Chargements.transform;

		obj1.GetComponent<RectTransform> ().offsetMin = new Vector2 (0,0);//left-bottom
		obj1.GetComponent<RectTransform> ().offsetMax = new Vector2 (0,0);//right-top

		obj2.GetComponent<RectTransform> ().offsetMin = new Vector2 (0,0);//left-bottom
		obj2.GetComponent<RectTransform> ().offsetMax = new Vector2 (0,0);//right-top
		}

		Text obj3;
		obj3 = Instantiate (texteVie) as Text;
		obj3.transform.name = "Texte Vie Archer";
		baseArcher.texteVie = obj3;
		obj3.transform.parent = Chargements.transform;

		obj3.GetComponent<RectTransform> ().offsetMin = new Vector2 (0,0);//left-bottom
		obj3.GetComponent<RectTransform> ().offsetMax = new Vector2 (0,0);//right-top


		baseArcher.nextActionTime= nextActionTime;
		baseArcher.period = periodChargement;

		baseArcher.peuple = new ArrayList();

		baseArcher.type = typeArcher;

		baseArcher.transform.name = nameArcher;

		basesArchers.Add (baseArcher);
		if (side == 0) {
			bases.Add (baseArcher);
		} else {
			basesEnnemy.Add (baseArcher);
		}
	}


	public void createBaseEpeiste(int side, Vector3 position){

		GameObject obj;
		BaseEpeiste baseEpeiste;
		Creator creator;
		obj = Instantiate (prefabBaseEpeiste, position, Quaternion.identity) as GameObject;
		obj.AddComponent<Creator> ();
		obj.AddComponent<BaseEpeiste> ();
		baseEpeiste = obj.GetComponent<BaseEpeiste> ();
		creator = obj.GetComponent<Creator> ();
		creator.typeCreator = typeCreatorEpeiste;
		creator.num = basesEpeistes.Count;

		creator.gv = this;
		creator.coutbois = coutBoisEpeiste;
		creator.coutfer = coutFerEpeiste;
		creator.coutnourriture = coutNourritureEpeiste;

		creator.targetRotation = targetRotation;
		creator.moveSpeed = moveSpeed;
		creator.rotateSpeed = rotateSpeed;
		creator.moving = moving;
		creator.rotating = rotating;


		creator.health = healthSoldatEpeiste;
		creator.currentHealth = currentHealthSoldatEpeiste;
		creator.attaque = attaqueSoldatEpeiste;

		creator.instantiateInit = position;
		creator.destinationInit = destinationInit;

		creator.space = space;
		creator.nbligne = nbLigne;

		creator.prefab1 = prefabSoldatEpeiste;
		creator.prefab2 = prefabSoldatEpeiste;
		creator.prefab3 = prefabSoldatCavalier;


		if (side == 0) {
			baseEpeiste.panel = panelBaseEpeiste;
		} else{
			baseEpeiste.panel = panelEnnemyEpeiste;
		}
		baseEpeiste.gv = this;
		baseEpeiste.health = healthBaseEpeiste;
		baseEpeiste.currentHealth = currentHealthBaseEpeiste;
		baseEpeiste.side = side;

		baseEpeiste.timerMax = timerMax;
		baseEpeiste.timer = timer;
		baseEpeiste.isValid = isValid;
		baseEpeiste.timerValide = timerValide;
		baseEpeiste.timerValideMax = timerValideMax;

		if (side == 0){
		GameObject obj1;
		obj1 = Instantiate (barreChargement) as GameObject;
		obj1.transform.parent = Chargements.transform;
		obj1.transform.name = "Barre File Attente Epeiste";
		baseEpeiste.barreChargement = obj1;
		Text obj2;
		obj2 = Instantiate (texteChargement) as Text;
		baseEpeiste.texteChargement = obj2;
		obj2.transform.name = "Texte File Attente Epeiste";
		obj2.transform.parent = Chargements.transform;

			obj1.GetComponent<RectTransform> ().offsetMin = new Vector2 (0,0);//left-bottom
			obj1.GetComponent<RectTransform> ().offsetMax = new Vector2 (0,0);//right-top

			obj2.GetComponent<RectTransform> ().offsetMin = new Vector2 (0,0);//left-bottom
			obj2.GetComponent<RectTransform> ().offsetMax = new Vector2 (0,0);//right-top
		}

		Text obj3;
		obj3 = Instantiate (texteVie) as Text;
		obj3.transform.name = "Texte Vie Epeiste";
		baseEpeiste.texteVie = obj3;
		obj3.transform.parent = Chargements.transform;

		obj3.GetComponent<RectTransform> ().offsetMin = new Vector2 (0,0);//left-bottom
		obj3.GetComponent<RectTransform> ().offsetMax = new Vector2 (0,0);//right-top

		baseEpeiste.nextActionTime = nextActionTime;
		baseEpeiste.period = periodChargement;

		baseEpeiste.peuple = new ArrayList ();

		baseEpeiste.type = typeEpeiste;

		baseEpeiste.transform.name = nameEpeiste;
		basesEpeistes.Add (baseEpeiste);

		if (side == 0) {
			bases.Add (baseEpeiste);
		} else {
			basesEnnemy.Add (baseEpeiste);
		}

	}


	public void createBaseCavalier(int side, Vector3 position){

		GameObject obj;
		BaseCavalier baseCavalier;
		Creator creator;
		obj = Instantiate (prefabBaseCavalier, position, Quaternion.identity) as GameObject;
		obj.AddComponent<Creator> ();
		obj.AddComponent<BaseCavalier> ();
		baseCavalier = obj.GetComponent<BaseCavalier> ();
		creator = obj.GetComponent<Creator> ();
		creator.typeCreator = typeCreatorCavalier;
		creator.num = basesCavaliers.Count;

		creator.gv = this;
		creator.coutbois = coutBoisCavalier;
		creator.coutfer = coutFerCavalier;
		creator.coutnourriture = coutNourritureCavalier;

		creator.targetRotation = targetRotation;
		creator.moveSpeed = moveSpeed;
		creator.rotateSpeed = rotateSpeed;
		creator.moving = moving;
		creator.rotating = rotating;


		creator.health = healthSoldatCavalier;
		creator.currentHealth = currentHealthSoldatCavalier;
		creator.attaque = attaqueSoldatCavalier;

		creator.instantiateInit = position;
		creator.destinationInit = destinationInit;

		creator.space = space;
		creator.nbligne = nbLigne;

		creator.prefab1 = prefabSoldatCavalier;
		creator.prefab2 = prefabSoldatCavalier;
		creator.prefab3 = prefabSoldatCavalier;

		if (side == 0) {
			baseCavalier.panel = panelBaseCavalier;
		} else{
			baseCavalier.panel = panelEnnemyCavalier;
		}

		baseCavalier.gv = this;
		baseCavalier.health = healthBaseCavalier;
		baseCavalier.currentHealth = currentHealthBaseCavalier;
		baseCavalier.side = side;

		baseCavalier.timerMax = timerMax;
		baseCavalier.timer = timer;
		baseCavalier.isValid = isValid;
		baseCavalier.timerValide = timerValide;
		baseCavalier.timerValideMax = timerValideMax;

		if (side == 0){
		GameObject obj1;
		obj1 = Instantiate (barreChargement) as GameObject;
		obj1.transform.parent = Chargements.transform;
		obj1.transform.name = "Barre File Attente Cavalier";
		baseCavalier.barreChargement = obj1;
		Text obj2;
		obj2 = Instantiate (texteChargement) as Text;
		baseCavalier.texteChargement = obj2;
		obj2.transform.name = "Texte File Attente Cavalier";
		obj2.transform.parent = Chargements.transform;

			obj1.GetComponent<RectTransform> ().offsetMin = new Vector2 (0,0);//left-bottom
			obj1.GetComponent<RectTransform> ().offsetMax = new Vector2 (0,0);//right-top

			obj2.GetComponent<RectTransform> ().offsetMin = new Vector2 (0,0);//left-bottom
			obj2.GetComponent<RectTransform> ().offsetMax = new Vector2 (0,0);//right-top

		}

		Text obj3;
		obj3 = Instantiate (texteVie) as Text;
		obj3.transform.name = "Texte Vie Cavalier";
		baseCavalier.texteVie = obj3;
		obj3.transform.parent = Chargements.transform;

		obj3.GetComponent<RectTransform> ().offsetMin = new Vector2 (0,0);//left-bottom
		obj3.GetComponent<RectTransform> ().offsetMax = new Vector2 (0,0);//right-top


		baseCavalier.nextActionTime= nextActionTime;
		baseCavalier.period = periodChargement;

		baseCavalier.peuple = new ArrayList();

		baseCavalier.type = typeCavalier;

		baseCavalier.transform.name = nameCavalier;

		basesCavaliers.Add (baseCavalier);

		if (side == 0) {
			bases.Add (baseCavalier);
		} else {
			basesEnnemy.Add (baseCavalier);
		}
	}



}
