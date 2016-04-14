using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeneratorManager : MonoBehaviour {

	// Use this for initialization
	public static int numberGenerator=10;
	public GameObject particleGenerator;
	public float champExclusion=20*0.5f;
	public float courbe=1;
	public float rapprochementZCourbe=1;
	int lastIndexDropGenerator = 0;
	public float percentDownGenerator =2;

	public float reducePercentPerDrop = 10;
	//liste des particles possibles
	public GameObject[] particles;
	//tableau des particles tirables en jeu avec leur valeur de tirage associée (%)
	private List<GameObject> getableParticle = new List<GameObject>();
	private List<float> purcentParticle = new List<float>();
	public List<GameObject> listGenerator = new List<GameObject>();
	public List<float> ponderation = new List<float>();

	public int sTimeAddingParticle = 2;
	private int countTimer = 0;
	private int addingIndex = 0;

	public int particleCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

    public Coroutine SpawnWavesCoroutine;

	void Start () {
		setGenerator ();
		AddParticleToGame ();
        SpawnWavesCoroutine = StartCoroutine(SpawnWaves ());

	}
	
	// Update is called once per frame
	void Update () {
		countTimer++;

		if (countTimer%200 == 0 && addingIndex != particles.Length)
		{
			print(" new Particle added");
			AddParticleToGame();
		}
	}

	void setGenerator(){
		int percentDrop = (int) 100 / numberGenerator;
		Vector3 size =GetComponent<Renderer>().bounds.size;
		Vector3 center = transform.position;
		float pas = (180-(champExclusion*2)) / numberGenerator;

		for (int i = 0; i <= numberGenerator; i++) { 
			float angle = champExclusion + pas * i -90;
			Vector3 pos = PointOnCircle(center, size.x/2,angle);
			Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center-pos);
			GameObject childObject = Instantiate (particleGenerator, pos, rot) as GameObject;
			listGenerator.Add (childObject);
			ponderation.Add (percentDrop);
		}

	}

	Vector3 PointOnCircle ( Vector3 center ,   float radius , float ang){
		Vector3 pos;
		pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
		pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad*courbe)*rapprochementZCourbe;
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
				GameObject particle = GetParticle ();
				GameObject generator = aleatoire_pondere (listGenerator, ponderation);
				generator.GetComponent<ParticleGenerator> ().CreateParticle (particle);
				ManagePurcentRetired (lastIndexDropGenerator, percentDownGenerator, ponderation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
		}
	}
    

	GameObject GetParticle()
	{
		float random = Random.Range(0, 100);
		float sum = 0;
		GameObject selectedParticle = getableParticle[0];
		if (purcentParticle.Count > 1) {
			for (int i = 0; i < purcentParticle.Count; i++) {
				sum += purcentParticle [i];
				if (sum > random) {
					selectedParticle = getableParticle [i];
					float purcentRetired = purcentParticle [i] * reducePercentPerDrop / 100;

					ManagePurcentRetired (i, purcentRetired, purcentParticle);

					break;
				}
			}
		}

		return selectedParticle;

	}

	void ManagePurcentRetired(int index, float purcent , List<float> listPurcent)
	{
		for (int i = 0; i < listPurcent.Count; i++)
		{
			if (i != index && listPurcent.Count>1) listPurcent[i] += purcent / (listPurcent.Count-1);
			else listPurcent[i] -= purcent;
		}

	}




	void AddParticleToGame()
	{ 
		 GameObject addedParticle = particles[addingIndex];

		//si c'est la première particule du jeu, elle démarre avec 100%
		if (getableParticle.Count == 0)
		{
			purcentParticle.Add(100);
			getableParticle.Add(addedParticle);
		}
		//sinon, on divise le pourcentage des particules présente pour qu'elle démarre avec un % adapté aux autres
		else
		{
			float purcent = 0;

			for (int i = 0; i < getableParticle.Count; i++)
			{
				float purcentPart = purcentParticle[i] / (addingIndex + 1);


				purcent += purcentPart;

				purcentParticle[i] -= purcentPart;
			}

			getableParticle.Add(addedParticle);
			purcentParticle.Add(purcent);
		}  

	addingIndex++;

	}

    public void StopCoroutineWave()
    {
        print("StopCoroutineWave");
        
    }



	GameObject aleatoire_pondere( List<GameObject> generators, List<float> ponderations )
	{
		float sumPonderations = 0;

		float random = Random.Range (0, 100);
		GameObject selected = generators[0];

		for (int i = 0; i < ponderations.Count; i++) {
			sumPonderations += ponderations [i];
			if (sumPonderations > random) 
			{
				selected = generators [i];
				lastIndexDropGenerator = i;
				break;
			}
		}
		return selected;
	}

	void stopWaves (){
		
	}

}
