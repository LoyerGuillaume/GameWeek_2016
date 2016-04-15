using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleGenerator : MonoBehaviour {

	// Use this for initialization
	public float startSpeed = 25000;
	public float growSpeed = 0.00015f;



	void Start () {
        gameObject.transform.parent = GameObject.Find("ParticleContainer").transform;
	}
	
	// Update is called once per frame
	void Update () {
		DifficultyGrow ();
	}

	public void CreateParticle(GameObject type){
		GameObject local = GameObject.Instantiate (type,transform.position,Quaternion.identity) as GameObject;
		local.GetComponent<Particle> ().InitSpeed (startSpeed);

	}

	void DifficultyGrow (){
		startSpeed = startSpeed - (startSpeed *growSpeed);
	}
}
