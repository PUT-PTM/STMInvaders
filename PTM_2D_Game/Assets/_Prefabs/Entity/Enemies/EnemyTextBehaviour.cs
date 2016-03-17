using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyTextBehaviour : MonoBehaviour {
	public Transform target;

	private Transform pos;
	private Text text;
	// Use this for initialization
	void Start () {
		if (!target) {
			Debug.LogError("Not added target reference!!!");
			gameObject.SetActive(false);
			return;
		}
		pos = GetComponent<Transform>();
		pos.position = new Vector3(target.position.x, target.position.y + 12);
		text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if (target) {
			pos.position = new Vector3(target.position.x, target.position.y + 12);
		} else text.text = "You killed me!!!";
	}
}
