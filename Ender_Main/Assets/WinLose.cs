using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinLose : MonoBehaviour {
	public RUISPSMoveWand wand1;
	public RUISPSMoveWand wand2;

	public Text text;

	float startTime;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (startTime != 42 && Time.time - startTime > 15) {
			text.text = "";
			startTime = 42;
		}
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("EnemyShip");
		GameObject[] allied = GameObject.FindGameObjectsWithTag ("Ship");
		if (enemies.Length == 0) {
			text.text = "Game Over! \nYou Win!";
		} else if (allied.Length == 0){
			text.text = "Game Over! \nYou Lose!";
		}
		if(wand1.startButtonWasPressed || wand2.startButtonWasPressed)
			Application.LoadLevel(Application.loadedLevel);
	}
}
