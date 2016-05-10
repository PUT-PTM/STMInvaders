using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawnerBehaviour : MonoBehaviour {
	public Canvas canvas;
	public Text textForEnemy;
	public Level[] levels = new Level[0];
	public Transform[] enemies = new Transform[0];
	public int forceSpawnedEnemy;

	// temporary flag, wanna do this better
	private bool tempFlag = true;
	public List<Transform> listOfShips;

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
			flyDelay = Random.Range(flyDelayMin, flyDelayMax);
		} else {
			flyDelay -= Time.deltaTime;
		}
	}

	private void StartRandomShipFlying() {
		if (listOfShips.Count > 0) {
			int index = Random.Range(0, listOfShips.Count - 1);
			listOfShips[index].GetComponent<EnemyMovement>().FlyNow();
			if (Random.Range(0, 10) < 3) StartRandomShipFlying();
		}
	}

	public void KillMe(Transform shipToKill) {
		listOfShips.Remove(shipToKill);
		Destroy(shipToKill.gameObject);
	}

	public Transform CreateClone(int i, Vector3 pos) {
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
}
