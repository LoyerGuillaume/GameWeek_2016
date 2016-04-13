using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeneratorManager : MonoBehaviour {

	// Use this for initialization
	public static int numberGenerator=10;
	public GameObject particleGenerator;
	public float champExclusion=20;

	public GameObject[] particles;
	public int particleCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	void Start () {
		setGenerator ();
		StartCoroutine (SpawnWaves ());
	}
	
	// Update is called once per frame
	void Update () {
	}

	void setGenerator(){
		Vector3 size =GetComponent<Renderer>().bounds.size;
		Vector3 center = transform.position;
		float pas = (180-(champExclusion*2)) / numberGenerator;

		for (int i = 0; i <= numberGenerator; i++) { 
			float angle = champExclusion + pas * i -90;
			Vector3 pos = PointOnCircle(center, size.x/2,angle);
			Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center-pos);
			GameObject childObject = Instantiate (particleGenerator, pos, rot) as GameObject;
			ParticleGenerator.listGenerator.Add (childObject);
		}
	}

	Vector3 PointOnCircle ( Vector3 center ,   float radius , float ang){
		Vector3 pos;
		pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
		pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
		pos.y = center.y;
		return pos;
	}

	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < particleCount; i++)
			{
				GameObject particle = particles[Random.Range (0, particles.Length)];
				print (particle);
				ParticleGenerator.listGenerator [Random.Range(0,ParticleGenerator.listGenerator.Count)].GetComponent<ParticleGenerator> ().CreateParticle (particle);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
		}
	}


}
