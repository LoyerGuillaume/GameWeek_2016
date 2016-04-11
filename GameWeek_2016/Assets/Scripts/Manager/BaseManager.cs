using UnityEngine;
using System.Collections;
using System;

public abstract class BaseManager<T> : MonoBehaviour where T : Component
{
    //sorte de classe Observer de base ... qui observe le GameManager

    public static T manager;

    public bool dontDestroyGameObjectOnLoad = false;

    [HideInInspector]
    public bool IsReady { get; protected set; }

    protected Transform _transform;

    protected virtual void Awake()
    {
        if (manager != null)
            Destroy(manager.gameObject);

        manager = this as T;

        if (dontDestroyGameObjectOnLoad) DontDestroyOnLoad(gameObject);

        IsReady = false;
        _transform = transform;

    }

    protected virtual void OnDestroy()
    {
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(CoroutineStart());
    }

    protected abstract IEnumerator CoroutineStart();
    

}
