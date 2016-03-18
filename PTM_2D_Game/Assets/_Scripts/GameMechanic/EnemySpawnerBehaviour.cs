using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemySpawnerBehaviour : MonoBehaviour {
	public Canvas canvas;
	public Text text;
	public Transform[] enemies = new Transform[0];

	// temporary flag, wanna do this better
	private bool tempFlag = true;

	void Start () {
		if (!canvas) {
			Debug.LogError("Not added canvas reference!!!");
			gameObject.SetActive(false);
			return;
		}
		if (!text) {
			Debug.LogError("Not added text reference!!!");
			gameObject.SetActive(false);
			return;
		}
	}
	// At this moment spawn only one enemy for tests
	void Update () {
		if (tempFlag) {
			//cloning objects
			Transform enemy = Instantiate(enemies[0]);
			Text textForClone = Instantiate(text);

			//setting parents
			textForClone.transform.SetParent(canvas.transform);
			enemy.SetParent(GetComponent<Transform>());

			//other actions
			enemy.GetComponent<EnemyBehaviour>().text = textForClone;
			enemy.position = GetComponent<Transform>().position;
			textForClone.GetComponent<EnemyTextBehaviour>().target = enemy;


			tempFlag = false;
		}
	}
}
