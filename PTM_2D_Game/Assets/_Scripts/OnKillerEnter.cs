using UnityEngine;
using System.Collections;

public class OnKillerEnter : MonoBehaviour {
	private Rigidbody2D rb;
	public float power;
	public float power2;

	// Use this for initialization
	void Start() {
		rb = GetComponent<Rigidbody2D>();
		float val1 = Random.Range(-1, 1) * Random.value;
		float val2 = Random.Range(-1, 1) * Random.value;
		rb.AddForce(new Vector2(val1 == 0 ? 1 : val1, val2 == 0 ? 1 : val2) * power * power);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		//rb.velocity = new Vector2(rb.velocity.x, 0);
		//rb.AddForce(Vector2.up * power, ForceMode2D.Impulse);
		Vector2 POWER = new Vector2(-rb.velocity.x, -rb.velocity.y);
		rb.velocity = new Vector2(0, 0);
		rb.AddForce(POWER * power2, ForceMode2D.Impulse);		
	}
}
