using UnityEngine;
using System.Collections;

public class LevelManager : BaseManager<LevelManager> {

    private const string PATH_LEVEL = "Levels/";

    public GameObject currentLevel;

    protected override IEnumerator CoroutineStart()
    {
        IsReady = true;
        yield return null;
    }

    public void StartLevel()
    {
        print("LevelManager - StartLevel");
        currentLevel = Instantiate(Resources.Load(PATH_LEVEL + "LevelLeapMotion")) as GameObject;
    }
    

}
