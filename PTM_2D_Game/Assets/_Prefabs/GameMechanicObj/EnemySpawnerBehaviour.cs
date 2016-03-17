using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemySpawnerBehaviour : MonoBehaviour {
	public Canvas canvas;
	public Text text;
	public Transform[] enemies = new Transform[0];

	private bool dupa = true;
	// Use this for initialization
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
	
	// Update is called once per frame
	void Update () {
		if (dupa) {
			Transform clone = Instantiate(enemies[0]);
			Text textForClone = Instantiate(text);
			textForClone.transform.SetParent(canvas.transform);
			clone.GetComponent<EnemyBehaviour>().text = textForClone;
			textForClone.GetComponent<EnemyTextBehaviour>().target = clone;


			dupa = false;
		}
	}
}
