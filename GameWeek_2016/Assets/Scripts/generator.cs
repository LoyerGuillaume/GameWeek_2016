using UnityEngine;
using System.Collections;

public class generator : MonoBehaviour 
{
	public GameObject particles; 

	// Use this for initialization
	void Start () 
	{
		GameObject.Instantiate (particles); 
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
