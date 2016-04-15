using UnityEngine;
using System.Collections;

public class AltarStartGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}


    void OnTriggerEnter(Collider col)
    {
        StartCoroutine(MagnetObject(col.gameObject));
    }


    IEnumerator MagnetObject(GameObject obj)
    {
        obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        obj.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;


        Transform table = transform.FindChild("Table");
        float tableHalfWidth = table.localScale.x / 2;

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

        GameManager.manager.StartLevel();
    }


}
