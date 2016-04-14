using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleGenerator : MonoBehaviour {

    // Use this for initialization

	public static List<GameObject> listGenerator = new List<GameObject>();
	public static List<float> ponderation = new List<float>();

	void Start () {
        gameObject.transform.parent = GameObject.Find("ParticleContainer").transform;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void CreateParticle(GameObject type){
		GameObject.Instantiate (type,transform.position,Quaternion.identity);

	}
}
