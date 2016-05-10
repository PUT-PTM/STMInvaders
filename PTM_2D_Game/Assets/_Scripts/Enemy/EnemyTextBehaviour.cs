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
			if(ImDead == false) {
				Random.seed = (Time.frameCount);
				int random = Random.Range(0, 40);
				if (random < 10 && random > 6) text.text = "You killed me!!!";
				else if (random < 20 && random > 16) text.text = "I see light :o";
				else if (random < 30 && random > 26) text.text = "Shit, noooope!!!";
				else if (random < 40 && random > 36) text.text = "Mommy ;___;";
				ImDead = true;
			}
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
