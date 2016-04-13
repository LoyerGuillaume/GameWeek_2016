using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Altar : MonoBehaviour {

    public int lifeScore = 100;
    public int lifeDecrementation = 5;
    public int lifeIncrementation = 20;

    private float lifeTimer = 0;

    private float tableHalfWidth;
    private Transform table;

	// Use this for initialization
	void Start () {
        lifeTimer = 0;
        table = transform.FindChild("Table");
        tableHalfWidth = table.localScale.x / 2;
    }
	
	// Update is called once per frame
	void Update () {
        lifeTimer += Time.deltaTime;
        if (lifeTimer >= 1)
        {
            lifeTimer = 0;
            lifeScore -= lifeDecrementation;
            //print(lifeScore);
        }

        if (lifeScore <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }

    void OnTriggerEnter (Collider col)
    {
        lifeScore += lifeIncrementation;
        StartCoroutine(MagnetObject(col.gameObject));
    }

    IEnumerator MagnetObject (GameObject obj)
    {
        obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Vector3 initPosition = obj.transform.position;
        Vector3 targetPosition = table.position;
        targetPosition.y += 0.5f;
        targetPosition.x += Random.Range(-tableHalfWidth, tableHalfWidth);

        Quaternion initRotation = obj.transform.rotation;
        Quaternion targetRotation = table.rotation;

        float timer = 0;
        float endTime = 300;

        while (timer < endTime)
        {
            timer += Time.deltaTime * 1000;
            float percent = timer / endTime;
            obj.transform.position = Vector3.Lerp(initPosition, targetPosition, percent);
            obj.transform.rotation = Quaternion.Slerp(initRotation, targetRotation, percent);
            yield return null;
        }

        obj.transform.position = targetPosition;
        obj.transform.rotation = targetRotation;
        obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

}
