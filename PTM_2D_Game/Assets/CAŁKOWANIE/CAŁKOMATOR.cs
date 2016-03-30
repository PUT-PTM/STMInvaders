using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CAŁKOMATOR : MonoBehaviour {
	public float xp;
	public float xk;
	public float dx;
	public float accuracy;
	public Text result;

	private bool dupa;
	void Start() {
		result = GetComponent<Text>();
	}

	void LateUpdate() {
		dx = (xk - xp) / accuracy;
		result.text = Calc().ToString();
	}

	private float Func(float x) {
		return x * x + 5;
	}
	private float Calc() {
		float Całka = 0;
		for (float i = 1; i <= accuracy; i++) {
			Całka += Func(xp + i * dx);
		}
		return Całka * dx;
	}
}
