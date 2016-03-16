using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {
	private Transform pos;
	public float speed;

	private string type;
	// Use this for initialization
	void Start () {
		pos = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		switch (type) {
			case "EnemyBullet": pos.position -= Vector3.up * Time.deltaTime * speed;
				break;
			case "PlayerBullet": pos.position += Vector3.up * Time.deltaTime * speed;
				break;
		}
		
	}

	public void SetType(string type) {
		this.type = type;
		switch (type) {
			case "EnemyBullet": tag = "EnemyBullet";
				break;
			case "PlayerBullet": tag = "PlayerBullet";
				break;
		}
	}
}
