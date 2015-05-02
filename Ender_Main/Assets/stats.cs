using UnityEngine;
using System.Collections;

public class stats : MonoBehaviour {

	public int hitpoints;
	public GameObject gameobject;
	private UISlider slider;
	public Texture2D tex = null;
	private int width;
	
	void OnGUI() {
		Vector3 scrPos = Camera.main.WorldToScreenPoint(this.transform.position);
		GUI.DrawTexture(new Rect(scrPos.x - 100/2.0f, Screen.height - scrPos.y - 30/2.0f, width, 30), tex, ScaleMode.StretchToFill);

		//Sets texture for size, for example, 100x30

	}
	// Use this for initialization
	void Start () {
		width = 100;
	}
	
	// Update is called once per frame
	void Update () {
		
		//Gets coordinate our object on screen

	}

	void hit(int damage) {
		hitpoints = hitpoints - damage;
		if (hitpoints <= 0) {
			//destroy
		}
	}
}
