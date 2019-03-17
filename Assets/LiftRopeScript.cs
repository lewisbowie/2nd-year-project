using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftRopeScript : MonoBehaviour {

	public GameObject lift;
	private float scaleValue;
	public float scaleMultiplier;

	// Use this for initialization
	void Start () {
		transform.localScale = new Vector3 (1, 1, 1);
		scaleMultiplier = 62.35F;
	}
	
	// Update is called once per frame
	void Update () {
		scaleValue = lift.transform.localPosition.y;
		transform.localScale = new Vector3 (1, 1, 1 - scaleValue / scaleMultiplier);
	}
}
