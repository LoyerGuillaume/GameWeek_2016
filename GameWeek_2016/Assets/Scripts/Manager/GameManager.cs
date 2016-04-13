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

    public void RestartLevel()
    {
        LevelManager.manager.DestroyCurrentLevel();
        MenuManager.manager.StartMenu();
    }

    public void StartGameOver()
    {
        MenuManager.manager.StartGameOverMenu();
    }

    // Update is called once per frame
    void Update () {
	
	}
}
