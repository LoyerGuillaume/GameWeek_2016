﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public enum AltarState
{
    alive,
    angry,
    broken
}

public class Altar : MonoBehaviour {

    public int maxLifePoints = 100;
    public int lifePoints = 100;
    public int lifeDecrementation = 10;
    public int lifeIncrementation = 20;

    private float lifeTimer = 0;

    private float tableHalfWidth;
    private Transform table;

    public AltarState state;
    public bool isActive;

    public GameObject[] eyes;

	// Use this for initialization
	void Start () {
        isActive = true;
        state = AltarState.alive;
        lifeTimer = 0;
        table = transform.FindChild("Table");
        tableHalfWidth = table.localScale.x / 2;
    }
	
	// Update is called once per frame
	void Update () {

        if (state != AltarState.broken && isActive)
        {
            lifeTimer += Time.deltaTime;
            if (lifeTimer >= 1)
            {
                lifeTimer = 0;
                lifePoints -= lifeDecrementation;
            }

            if (state != AltarState.angry && lifePoints <= maxLifePoints / 2)
            {
                Angry();
            }

            if (lifePoints <= 0)
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
        lifePoints += lifePoints + lifeIncrementation;

        if (state == AltarState.broken)
        {
            Restore();
            LevelManager.manager.AltarRegen();
        }

        StartCoroutine(MagnetObject(col.gameObject));
    }

    void Restore ()
    {
        state = AltarState.alive;
        foreach (GameObject eye in eyes)
        {
            eye.SetActive(false);
        }
    }

    void Angry ()
    {
        state = AltarState.angry;
    }

    void Broke ()
    {
        lifePoints = 0;
        state = AltarState.broken;
        foreach (GameObject eye in eyes)
        {
            eye.SetActive(true);
        }
    }





    IEnumerator MagnetObject(GameObject obj)
    {
        obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        obj.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        Vector3 initPosition = obj.transform.position;
        Vector3 targetPosition = table.position;
        targetPosition.y += 0.5f;
        targetPosition.x += Random.Range(-tableHalfWidth, tableHalfWidth);

        Quaternion initRotation = obj.transform.rotation;
        Quaternion targetRotation = table.rotation;

        Vector3 initScale = obj.transform.localScale;
        Vector3 targetScale = Vector3.one;

        float timer = 0;
        float endTime = 1300;

        while (timer < endTime)
        {
            timer += Time.deltaTime * 1000;
            float percent = timer / endTime;
            obj.transform.position = Vector3.Lerp(initPosition, targetPosition, percent);
            obj.transform.rotation = Quaternion.Slerp(initRotation, targetRotation, percent);
            obj.transform.localScale = Vector3.Lerp(initScale, targetScale, percent);
            yield return null;
        }

        obj.transform.position = targetPosition;
        obj.transform.rotation = targetRotation;
        obj.transform.localScale = targetScale;
        obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

}
