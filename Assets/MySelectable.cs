using UnityEngine;
using System.Collections;

public class MySelectable : RUISSelectable {
	float initialscale;
	RUISWandSelector selector;
	Vector3 originalscale;
	Vector3 mo = new Vector3 (0.0f, 1.7f ,-4.9f);
	bool started = false;
	RUISPSMoveWand movewand;
	public override void OnSelection(RUISWandSelector selector) {
		this.selector = selector;
		if (selector.psmove2)
				movewand = selector.psmove2.GetComponent<RUISPSMoveWand> ();
		else
			movewand = null;
		started = false;
		base.OnSelection (selector);
	}

	protected override void UpdateTransform(bool safePhysics) {
		if (!isSelected) {
			started = false;
			return;
		}

		if (movewand && movewand.SelectionButtonIsDown()){
		//if (Input.GetKey(KeyCode.A)){
			if (!started)  {
				Vector3 a1 = selector.transform.position;
				Vector3 b1 = selector.psmove2.transform.position;
				initialscale = Mathf.Abs ((a1 - b1).magnitude);
				originalscale = this.transform.localScale;
				Debug.Log (a1);
				Debug.Log (initialscale);
				started = true;
			}	

			Vector3 a = selector.transform.position;
			Vector3 b = selector.psmove2.transform.position;
			float newlength = Mathf.Abs ((a - b).magnitude);
			Debug.Log (a);
			Debug.Log (newlength);
			float scale = (newlength / initialscale);
			Debug.Log (scale);

			this.transform.localScale = originalscale * scale;
		}
		else
			started = false;
		base.UpdateTransform (safePhysics);
	}
}
