using UnityEngine;
using System.Collections;
using System;

public class EnemyMovement : MonoBehaviour {
	#region Public Variables
	public float speedLR;	//speed of ship (left or right)
	public float speedUD;   //speed of ship (up or down)
	public float delta;     //distance from "startPos" while LR flying
	public bool dirLR;		//direction of flying (left or right)
	public bool CanShoot { get { return canShoot; } }
	#endregion
	#region Private Variables
	//if false then fly left, else fly right
	private static bool LR_STATE = false;
	private Transform enemy;	//enemy transform
	private bool canShoot;		//says if ship can shoot or not(basis on flying mode)
	private Action flyingMode;	//flying-mode delegate
	private Vector2 startPos;   //position of ship at start
	private bool assault;		//flag of assault flying mode
	private float x1, x2, a;    //variables for calculate pos. while assault
	private bool flyTemp;       //additional flag (whatever - it works :P )
	private bool endAssault;    //flag needed to easy back flying LR again
	private bool lastDirLR;		//needed to synchronize with other ships
	#endregion
	#region Start & Update
	void Start() {
		enemy = GetComponent<Transform>();
		startPos = new Vector2(enemy.position.x, enemy.position.y);
		SetSqareFooVars();
		flyingMode = FlyingLeft;
		canShoot = false;
		dirLR = false;
		assault = false;
	}

	void Update() {
		flyingMode();
	}
	#endregion
	#region Flying modes
	private void FlyingLeft() {
		if (!flyTemp) {
			enemy.position -= new Vector3(speedLR * Time.deltaTime, 0);
			if (enemy.position.x <= startPos.x - delta) {
				flyTemp = true;
			}
		} else {
			enemy.position += new Vector3(speedLR * Time.deltaTime, 0);
			if (enemy.position.x >= startPos.x) {
				dirLR = false;
				flyTemp = false;
				if (dirLR != LR_STATE) LR_STATE = false;
				if (!assault) flyingMode = FlyingRight;
				else {
					enemy.position = startPos;
					flyingMode = Assault;
					canShoot = true;
				}
			}
		}
	}
	private void FlyingRight() {
		if (!flyTemp) {
			enemy.position += new Vector3(speedLR * Time.deltaTime, 0);
			if (enemy.position.x >= startPos.x + delta) {
				flyTemp = true;
			}
		}
		else {
			enemy.position -= new Vector3(speedLR * Time.deltaTime, 0);
			if (enemy.position.x <= startPos.x) {
				dirLR = true;
				flyTemp = false;
				if (dirLR != LR_STATE) LR_STATE = true;
				if (!assault) flyingMode = FlyingLeft;
				else {
					enemy.position = startPos;
					flyingMode = Assault;
					canShoot = true;
				}
			}
		}
	}
	private void Assault() {
		if (!endAssault) enemy.position = CountPosition();
		else {
			float y = enemy.position.y;
			if (y > startPos.y - 1.5f && y < startPos.y + 1.5f) {
				flyingMode = BackToStart;
				lastDirLR = LR_STATE;
			}
			else enemy.position = CountPosition();
		}
	}
	private void BackToStart() {
		if (lastDirLR != LR_STATE) {
			endAssault = false;
			assault = false;
			canShoot = false;
			dirLR = LR_STATE;
			enemy.position = startPos;
			if (!dirLR) flyingMode = FlyingRight;
			else flyingMode = FlyingLeft;
			flyingMode();
		}
	}
	#endregion
	#region Public functions
	public void StartAssault() {
		assault = true;
	}
	public void EndAssault() {
		endAssault = true;
	}
	public void LetMeGo() {

	}
	#endregion
	#region Private Functions
	private Vector3 CountPosition() {
		float y = enemy.position.y - speedUD * Time.deltaTime;
		float result = a * (y - (x2)) * (y - (x1));
		return new Vector3(result + startPos.x, y);
	}
	private void SetSqareFooVars() {
		x2 = startPos.y;
		x1 = x2 - 150f;
		if (startPos.x > 0) a = 0.015f;
		else a = -0.015f;
	}
	#endregion
}
