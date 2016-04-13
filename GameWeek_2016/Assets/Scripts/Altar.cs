using UnityEngine;
using System.Collections;

public class Altar : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter (Collider col)
    {
        StartCoroutine(MagnetObject(col.gameObject));
    }

    IEnumerator MagnetObject (GameObject obj)
    {
        obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Vector3 initPosition = obj.transform.position;

        float timer = 0;
        float endTime = 300;

        while (timer < endTime)
        {
            timer += Time.deltaTime * 1000;
            float percent = timer / endTime;
            obj.transform.position = Vector3.Lerp(initPosition, transform.position, percent);
            yield return null;
        }
    }
}
