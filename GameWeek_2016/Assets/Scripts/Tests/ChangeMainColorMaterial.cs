using UnityEngine;
using System.Collections;

public class ChangeMainColorMaterial : MonoBehaviour {

    Color currentColor;
    public float speedDarknessColor = 0.05f;

	// Use this for initialization
	void Start () {
        currentColor = GetComponent<Renderer>().material.color;
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Renderer>().material.color = new Color(currentColor.r - speedDarknessColor * Time.deltaTime, currentColor.g - speedDarknessColor * Time.deltaTime, currentColor.b - speedDarknessColor * Time.deltaTime);
        currentColor = GetComponent<Renderer>().material.color;
    }
}
