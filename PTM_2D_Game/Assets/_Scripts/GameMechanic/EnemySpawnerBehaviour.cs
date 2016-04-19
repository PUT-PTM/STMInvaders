using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemySpawnerBehaviour : MonoBehaviour {
	public Canvas canvas;
	public Text textForEnemy;
	public Level[] levels = new Level[0];
	public Transform[] enemies = new Transform[0];
	public int forceSpawnedEnemy;

	// temporary flag, wanna do this better
	private bool tempFlag = true;

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
	}
	
	// At this moment spawn static countof enemies for tests
	void Update () {
		if (tempFlag) {
			float posX = -110f;
			float posY = 10f;
			int num = 0;
			for (int i = 0; i < 2; i++) {
				for (int j = 0; j < 5; j++) {
					CreateClone(num++, new Vector3(posX, posY));

					posX += 20f;
				}
				posX = -110f;
				posY += 20f;
			}
			tempFlag = false;
		}
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
		enemy.position = GetComponent<Transform>().position;
		textForClone.GetComponent<EnemyTextBehaviour>().target = enemy;

		//set something...
		enemy.GetComponent<EnemyBehaviour>().ID = "dupa" + i;
		enemy.GetComponent<EnemyBehaviour>().enemy.position = pos;
		enemy.GetComponent<EnemyBehaviour>().text.text = "dupa" + i;
		enemy.name = "dupa" + i;

		return enemy;
	}
}
