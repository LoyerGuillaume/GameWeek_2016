using UnityEngine;
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

    //private float lifeTimer = 0;
    private float lastElapsedTime;

    private float tableHalfWidth;
    private Transform table;

    public AltarState state;

    public GameObject[] eyes;

    public GameObject spotAngry;

    public FlowerType typeFlower;

	// Use this for initialization
	void Start () {
        state = AltarState.alive;
        lastElapsedTime = 0;
        table = transform.FindChild("Table");
        tableHalfWidth = table.localScale.x / 2;
    }
	
	// Update is called once per frame
	void Update () {

        if (state != AltarState.broken)
        {
            if (LevelManager.manager.score - lastElapsedTime >= 1)
            {
                lastElapsedTime = LevelManager.manager.score;
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
        if (col.gameObject.GetComponent<Particle>().flowerType != typeFlower) return;

        if (SfxManager.manager) SfxManager.manager.PlaySfx("FlowerInAltar");

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
        lastElapsedTime = LevelManager.manager.score;
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
        if (SfxManager.manager) SfxManager.manager.PlaySfx("AltarAngry");

        lifePoints = 0;
        state = AltarState.broken;

        StartCoroutine(SpotAngryCoroutine());

        foreach (GameObject eye in eyes)
        {
            eye.SetActive(true);
        }
    }

    IEnumerator SpotAngryCoroutine()
    {
        float elapsedTime = 0;
        float duration = 0.25f;

        while (elapsedTime < duration)
        {
            float intensity = Mathf.Cos(0.5f + elapsedTime / duration) * 8;
            spotAngry.GetComponent<Light>().intensity = intensity;
            print(intensity);
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        spotAngry.GetComponent<Light>().intensity = 0;

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

        obj.GetComponent<Particle>().StartRotten();
    }

}
