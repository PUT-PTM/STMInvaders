using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
	#region Variables
	public Transform[] obj = new Transform[0];
	public int moveStart;	//width between zeroes of function
	public int moveEnd;		//basically points in space to move ship

	private Transform tr;
	private Transform temp;
	private Vector2 position;
	private float posX, pos;
	public bool dir;

	//tylko tymczasowe flagi pomocnicze, coś na zasadzie - jak jest true to lataj i w którą stronę
	//TODO przerobić na delegaty
	private bool flyDir = true;     //ta określa kierunek (góra czy dół)
	private bool canShoot = false;  //ta pozwala lecieć
	public bool CanShoot { get { return canShoot; } }
	#endregion
	#region Start & Update
	void Start() {
		temp = tr = GetComponent<Transform>();
		position = temp.position;   //point of reference for function
		posX = -moveStart / 2;      //position X counting from start position
		pos = 0;                    //position Y counting from start position
		moveEnd = Random.Range(30, 100);
		//its just for help, with this flag enemy ships not hit in the walls
		if (position.x > 0) dir = false; else dir = true;
		StartPos = new Vector2(position.x, position.y);
		flyingEvent = FlyLeftRight;
	}
	void Update () {
		flyingEvent();
	}
	#endregion
	#region Others
	private Vector2 CalcPos(bool dir = true) {
		if(dir) return new Vector2(Func(posX++), -pos++);
		else return new Vector2(Func(posX--), -pos--);
	}
	private float Func(float x) {
		if (dir) return -0.01f * x * x + 55f;
		else return 0.01f * x * x - 55f;
	}
	#endregion
	#region Tryby lotu
	private delegate void FlyingMode();
	private FlyingMode flyingEvent;
	private Vector2 StartPos;
	private bool lrDir = true;

	void FlyAssault() {
		if (flyDir) {
			if (posX < moveEnd) {
				tr.position = position + CalcPos();
			}
			else flyDir = false;
		}
		else {
			if (posX > (-moveStart / 2)) {
				tr.position = position + CalcPos(false);
			}
			else {
				flyDir = true;
				flyingEvent = FlyLeftRight;
				canShoot = false;
			}
		}
	}
	void FlyLeftRight() {
		// sprawdza kierunek true = lewo
		if (lrDir) {
			tr.position -= new Vector3(0.25f, 0);
			if (tr.position.x < (StartPos.x - 5)) lrDir = false;
		}
		// jeśli nie to w prawo... :<
		else {
			tr.position += new Vector3(0.25f, 0);
			if (tr.position.x > (StartPos.x + 5)) lrDir = true;
		}
	}
	// change flying mode
	public void FlyNow() {
		flyingEvent = FlyAssault;
		tr.position = new Vector3(StartPos.x, StartPos.y);
		canShoot = true;
	}
	#endregion
}
