﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowEnemies : MonoBehaviour {
	public EnemySpawnerBehaviour enemySpawner;

	private Text text;
	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		text.text = " Enemies:\n 0";
	}
	
	// Update is called once per frame
	void LateUpdate () {
		int counter = enemySpawner.GetComponentsInChildren<EnemyBehaviour>().Length;
		if (text.text != (" Enemies:\n " + counter)) {
			text.text = " Enemies:\n " + counter;
		}
	}
}
