using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
	
	public Image image;

	public float minValue = 0.0f;
	public float maxValue = 0.1f;
	public Color minColor = Color.red;
	public Color maxColor = Color.green;

	// Use this for initialization
	void Start () {
		if (image == null){
			image = GetComponent<Image>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		image.color = Color.Lerp(minColor, maxColor, Mathf.Lerp(minValue, maxValue, transform.localScale.x));
	}

	void takeDamage(int damage) {

	}
}
