using UnityEngine;
using System.Collections;

public class StarsBehaviour : MonoBehaviour {
	public Transform stars1;
	public Transform stars2;
	public float speed;

	private float posCnt1;
	private float posCnt2;

	// Use this for initialization
	void Start() {
		posCnt1 = 0f;
		posCnt2 = 225f;
	}

	// Update is called once per frame
	void Update() {
		// stars 1
		if (posCnt1 >= 475f) {
			stars1.position += new Vector3(0f, 475f);
			posCnt1 = 0;
		}
		else {
			stars1.position -= new Vector3(0f, speed * Time.deltaTime);
			posCnt1 += speed * Time.deltaTime;
		}
		// stars 2
		if (posCnt2 >= 475f) {
			stars2.position += new Vector3(0f, 475f);
			posCnt2 = 0;
		}
		else {
			stars2.position -= new Vector3(0f, speed * Time.deltaTime);
			posCnt2 += speed * Time.deltaTime;
		}
	}
}
