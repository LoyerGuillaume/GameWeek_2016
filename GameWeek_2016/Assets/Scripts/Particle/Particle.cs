using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour {

	// Use this for initialization
	public GameObject BoxTop ;

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (0f, 0.1f, 0f);
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.name == "BoxTop") {
			Destroy (gameObject);
		}

	}
}
