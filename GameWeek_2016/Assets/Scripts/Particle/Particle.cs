using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour {

	public AnimationCurve movement; 
	private float timer = 0; 
	private float endTime = 10000; 
	private float percent; 
	private Vector3 initialPosition; 
	private Vector3 currentPosition; 
	public float elevation = 5;

    private float rotationY = 1.5f;
    private float rotationZ = 0.1f;

    Coroutine moveUpCoroutine; 

	// Use this for initialization
	void Start () 
	{
        gameObject.transform.parent = GameObject.Find("ParticleContainer").transform;
        moveUpCoroutine = StartCoroutine(MoveUp ()); 
	}
	
	// Update is called once per frame
	void Update ()
	{
		// on donne un axe de rotation pour l'objet 
		transform.Rotate(0, rotationY, rotationZ, Space.World); 
	}

	IEnumerator MoveUp()
	{
		initialPosition = transform.position;

		timer = 0; 
		Vector3 targetPosition = initialPosition; 
		targetPosition.y += elevation;

		while (timer < endTime) 
		{
			timer += Time.deltaTime * 1000;
			percent = timer / endTime; 

			//on donne un comportement directionnel à la particule 
			transform.position = Vector3.Lerp (initialPosition, targetPosition, movement.Evaluate(percent)); 

			//taille initiale 
			//transform.localScale = new Vector3 (0.025f, 0.025f, 0.025f); 

			//changement scale 
			//transform.localScale += Vector3.Lerp(new Vector3(0.15f,0.15f,0.15f), new Vector3(1,1,1), movement.Evaluate(percent)); 

			yield return null; //coroutine en pause jusqu'à la prochaine frame  
		}
	}

    public void StopMovement()
    {
        StopCoroutine(moveUpCoroutine);
        rotationZ = 0;
        rotationY = 0;
    }
}