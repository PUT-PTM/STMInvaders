using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyTextBehaviour : MonoBehaviour {
	public Transform target;

	private Transform pos;
	private Text text;
	private bool ImDead = false;
	private float TimeToDead = 2f;

	void Start () {
		pos = GetComponent<Transform>();
		text = GetComponent<Text>();

		pos.position = new Vector3(target.position.x, target.position.y + 10);
		//text.text = "";
	}
	// Text follow enemy and when he dies, show message (at this moment)
	void Update () {
		// Follow enemy ship
		if (target) {
			pos.position = new Vector3(target.position.x, target.position.y + 10);
			// SetText(pos.position.ToString());	- just 4 tests
		} else {
			text.text = "You killed me!!!";
			ImDead = true;
		}
		// Shows text
		if (ImDead) {
			if (TimeToDead < 0) {
				Destroy(gameObject);
			} else TimeToDead -= Time.deltaTime;
		}
	}

	public void SetText(string text) {
		this.text.text = text;
	}
}
