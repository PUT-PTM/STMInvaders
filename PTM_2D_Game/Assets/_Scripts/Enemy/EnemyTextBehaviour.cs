using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyTextBehaviour : MonoBehaviour {
	public Transform target;

	private Transform pos;
	private Text text;
	// Use this for initialization
	void Start () {
		pos = GetComponent<Transform>();
		pos.position = new Vector3(target.position.x, target.position.y + 10);
		text = GetComponent<Text>();
		text.text = "___";
	}

	private bool ImDead = false;
	private float TimeToDead = 2f;
	// Update is called once per frame
	void Update () {
		if (target) {
			pos.position = new Vector3(target.position.x, target.position.y + 10);
		} else {
			text.text = "You killed me!!!";
			ImDead = true;
		}

		if (ImDead) {
			if (TimeToDead < 0) {
				Destroy(gameObject);
			} else TimeToDead -= Time.deltaTime;
		}
	}
}
