using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleGenerator : MonoBehaviour {

	// Use this for initialization


	public static List<GameObject> listGenerator = new List<GameObject>();
	public static ParticleGenerator[] arrayOfGenerator = new ParticleGenerator[GeneratorManager.numberGenerator+1];

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void CreateParticle(GameObject type){
		GameObject.Instantiate (type,transform.position,Quaternion.identity);

	}
}
