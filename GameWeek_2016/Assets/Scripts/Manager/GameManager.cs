using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    
    public static GameManager manager;

    bool infoMenuActive = false;

    public bool gameOver = false;

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
        gameOver = false;
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
        gameOver = true;
        LevelManager.manager.levelStart = false;
        MenuManager.manager.StartGameOverMenu();
    }


    public void HandDead()
    {
        int numberOfDeadHand = 0;

        if (!LevelManager.manager.leftHandAlive)
        {
            numberOfDeadHand++;
        }
        if (!LevelManager.manager.rightHandAlive)
        {
            numberOfDeadHand++;
        }

        if (numberOfDeadHand == 1)
        {
            infoMenuActive = true;
            MenuManager.manager.StartInfoHand();

        } else if (numberOfDeadHand == 2)
        {
            StartGameOver();
        }
    }
    
    public void RemoveInfoHand()
    {
        MenuManager.manager.DestroyCurrentMenu();
    }

    // Update is called once per frame
    void Update () {
	
	}
}
