using UnityEngine;
using System.Collections;

public class InGameMenu : MonoBehaviour {
	public float width;     // width of GUI window
	public float height;    // height of GUI window

	// x and y are sizes of screen
	// offset is part of field between controls and GUI borders
	private float x, y;
	private float offX, offY;
	private bool pause;

	void Start() {
		pause = false;
		x = Screen.width;
		y = Screen.height;
		offX = 0.1f * width;
		offY = 0.1f * height;
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			pause = !pause;
			if (pause) Time.timeScale = 0f;
			else Time.timeScale = 1f;
		}
	}
	void OnGUI() {
		if (pause) {
			GUI.Box(new Rect(x / 2 - width / 2, y / 2 - height / 2 - offY * 2, width, height * 0.7f), "Game menu");
			if (GUI.Button(new Rect(
					x / 2 - width / 2 + offX, y / 2 - height / 2 + offY * 0,
					width - offX * 2, offY * 4), "Quit game")) {
				Application.Quit();
			}
		}
	}
}