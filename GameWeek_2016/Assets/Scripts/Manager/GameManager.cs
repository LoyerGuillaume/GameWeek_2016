using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    
    public static GameManager manager;

    private GameManager() { }


    void Awake()
    {
        manager = this;
    }

    IEnumerator Start()
    {
        while (!(MenuManager.manager.IsReady) || !(LevelManager.manager.IsReady)) {
            yield return null;
        }
        
        MenuManager.manager.StartMenu();

    }

    public void StartLevel()
    {
        print("GameManager - StartLevel");
        LevelManager.manager.StartLevel();
    }

    // Update is called once per frame
    void Update () {
	
	}
}
