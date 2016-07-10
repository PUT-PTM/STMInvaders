using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadGameMenu : MonoBehaviour {
	public float width;     // width of GUI window
	public float height;    // height of GUI window

	// x and y are sizes of screen
	// offset is part of field between controls and GUI borders
	private float x, y;
	private float offX, offY;

	//TODO remove this bullshit
	public string keke;

	void Start() {
		//TODO stop movement
		x = Screen.width;
		y = Screen.height;
		offX = 0.1f * width;
		offY = 0.1f * height;
	}
	void OnGUI() {
		GUI.Box(new Rect(x / 2 - width / 2, y / 2 - height / 2 - offY * 4, width, height * 1.9f), "Game menu");
		GUI.Label(new Rect(
				x / 2 - width / 2 + offX, y / 2 - height / 2 + offY * -1,
				width - offX * 1, offY * 3), "Set port name");
		Statics.VCP_PORT = GUI.TextField(new Rect(
				x / 2 - width / 2 + offX, y / 2 - height / 2 + offY * 1,
				width - offX * 2, offY * 3), Statics.VCP_PORT);
		if (GUI.Button(new Rect(
				x / 2 - width / 2 + offX, y / 2 - height / 2 + offY * 5,
				width - offX * 2, offY * 4), "Start game")) {
			SceneManager.LoadScene("Level0");
		}
		if (GUI.Button(new Rect(
				x / 2 - width / 2 + offX, y / 2 - height / 2 + offY * 10,
				width - offX * 2, offY * 4), "Quit game")) {
			Application.Quit();
		}
	}
}