using UnityEngine;
using System.Collections;

public class LevelManager : BaseManager<LevelManager>
{


    protected override IEnumerator CoroutineStart()
    {
        IsReady = true;
        yield return null;
    }

    public void StartLevel()
    {
        print("LevelManager - StartLevel");
    }
    

}
