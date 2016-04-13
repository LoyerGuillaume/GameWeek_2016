using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Altar : MonoBehaviour {

    public int lifeScore = 100;
    public int lifeDecrementation = 10;
    public int lifeIncrementation = 20;

    private float lifeTimer = 0;

    private float tableHalfWidth;
    private Transform table;

    private bool isAlive;
    public bool isActive;

    public GameObject[] eyes;

	// Use this for initialization
	void Start () {
        isActive = true;
        isAlive = true;
        lifeTimer = 0;
        table = transform.FindChild("Table");
        tableHalfWidth = table.localScale.x / 2;
    }
	
	// Update is called once per frame
	void Update () {

        if (isAlive && isActive)
        {
            lifeTimer += Time.deltaTime;
            if (lifeTimer >= 1)
            {
                lifeTimer = 0;
                lifeScore -= lifeDecrementation;
            }

            if (lifeScore <= 0)
            {
                Broke();
                LevelManager.manager.AltarBroken();
            }
        } else
        {

        }
        
    }

    void OnTriggerEnter (Collider col)
    {
        lifeScore += lifeIncrementation;

        if (!isAlive)
        {
            Restore();
            LevelManager.manager.AltarRegen();
        }

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

    void Restore ()
    {
        isAlive = true;
        foreach (GameObject eye in eyes)
        {
            eye.SetActive(false);
        }
    }

    void Broke ()
    {
        isAlive = false;
        foreach (GameObject eye in eyes)
        {
            eye.SetActive(true);
        }
    }

}
