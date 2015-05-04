using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinLose : MonoBehaviour {
	
	public Text text;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("EnemyShip");
		GameObject[] allied = GameObject.FindGameObjectsWithTag ("Ship");
		if (enemies.Length == 0) {
			text.text = "Game Over! \nYou Win!";
		} else if (allied.Length == 0){
			text.text = "Game Over! \nYou Lose!";
		}
	}
}
