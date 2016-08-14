using UnityEngine;
using System.Collections;


public class Compass : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Input.compass.enabled = true;



	}
	
	// Update is called once per frame
	void Update () {
		this.transform.rotation = Quaternion.Euler(0, -Input.compass.trueHeading, 0);
	}
}
