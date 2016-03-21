using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowEnemies : MonoBehaviour {
	public EnemySpawnerBehaviour spawner;

	private Text text;
	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		text.text = "Enemies:\n0";
	}
	
	// Update is called once per frame
	void LateUpdate () {
		int dupa = spawner.GetComponentsInChildren<EnemyBehaviour>().Length;
		if (text.text != ("Enemies:\n" + dupa)) {
			text.text = "Enemies:\n" + dupa;
		}
	}
}
