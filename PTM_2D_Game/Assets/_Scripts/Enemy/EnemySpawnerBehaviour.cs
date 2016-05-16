using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemySpawnerBehaviour : MonoBehaviour {
	#region Variables
	public Canvas canvas;
	public Text textForEnemy;
	public Text gameStatText;
	public Level[] levels = new Level[0];
	public Transform[] enemies = new Transform[0];
	public int forceSpawnedEnemy;

	// temporary flag, wanna do this better
	private bool tempFlag = true;
	public List<Transform> listOfShips;
	#endregion
	#region Start & Update
	void Start () {
		if (!canvas) {
			Debug.LogError("Not added canvas reference!!!");
			gameObject.SetActive(false);
			return;
		}
		if (!textForEnemy) {
			Debug.LogError("Not added text reference!!!");
			gameObject.SetActive(false);
			return;
		}
		listOfShips = new List<Transform>();
	}
	private float flyDelay = 0;
	public float flyDelayMin;
	public float flyDelayMax;
	// At this moment spawn static countof enemies for tests
	void Update () {
		if (tempFlag) {
			float posX = -110f;
			float posY = 20f;
			int num = 0;
			for (int i = 0; i < 4; i++) {
				for (int j = 0; j < 10; j++) {
					listOfShips.Add(CreateClone(num++, new Vector3(posX, posY)));
					if (j == 4) posX += 40f;
					posX += 20f;
				}
				posX = -110f;
				posY += 20f;
			}
			tempFlag = false;
		}
		if(flyDelay < 0) {
			StartRandomShipFlying();
			flyDelay = UnityEngine.Random.Range(flyDelayMin, flyDelayMax);
		} else {
			flyDelay -= Time.deltaTime;
		}
		if (!(listOfShips.Count > 0)) {
			gameStatText.text = "Woah - U won!!!";
			gameStatText.gameObject.SetActive(true);
		}
	}
	#endregion
	#region Private functions
	private void StartRandomShipFlying() {
		if (listOfShips.Count > 0) {
			int index = UnityEngine.Random.Range(0, listOfShips.Count - 1);
			listOfShips[index].GetComponent<EnemyMovement>().FlyNow();
			if (UnityEngine.Random.Range(0, 10) < 3) StartRandomShipFlying();
		}
	}
	private Transform CreateClone(int i, Vector3 pos) {
		//cloning objects
		Transform enemy = Instantiate(enemies[forceSpawnedEnemy]);
		Text textForClone = Instantiate(textForEnemy);

		//setting parents
		textForClone.transform.SetParent(canvas.transform);
		enemy.SetParent(GetComponent<Transform>());

		//other actions
		enemy.GetComponent<EnemyBehaviour>().text = textForClone;
		textForClone.GetComponent<EnemyTextBehaviour>().target = enemy;

		//set others
		enemy.position = pos;
		enemy.gameObject.SetActive(true);
		enemy.GetComponent<EnemyBehaviour>().text.text = "";
		enemy.name = "Enemy" + i;

		return enemy;
	}
	#endregion
	#region Public functions
	public void ClearList() {
		for (int i = 0; i < listOfShips.Count; i++) {
			Destroy(listOfShips[i].GetComponent<Transform>().gameObject);
		}
		listOfShips.Clear();
	}

	public void KillMe(Transform shipToKill) {
		listOfShips.Remove(shipToKill);
		Destroy(shipToKill.gameObject);
	}
	#endregion
}
