using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifesScript : MonoBehaviour {
	public GameObject[] lifesTab = new GameObject[4];
	public Text lifes;
	public string text {
		get { return lifes.text; }
		set { lifes.text = value; }
	}

	void Awake() {
		lifes = GetComponent<Text>();
	}
	public void UPDATE() {
		lifes.text = "  Lifes left";
		for (int i = 0; i < lifesTab.Length; i++) {
			if(i < Statics.PLAYER_LIFES) {
				lifesTab[i].SetActive(true);
			}
			else lifesTab[i].SetActive(false);
		}
	}
}
