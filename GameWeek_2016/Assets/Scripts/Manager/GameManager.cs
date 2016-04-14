using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    
    public static GameManager manager;

    bool infoMenuActive = false;

    bool mainMenuActive = false;

    public bool gameOver = false;


    public float speedCamera = 2;

    private Quaternion startRotationCamera;

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

        startRotationCamera = Camera.main.transform.rotation;

        MenuManager.manager.StartMenu();
        StartRotationMenu();
        mainMenuActive = true;
    }

    public void StartLevel()
    {
        gameOver = false;
        print("GameManager - StartLevel");
        mainMenuActive = false;
        StartCoroutine(ReinitializeCamera());
        ReinitializeCamera();
        LevelManager.manager.StartLevel();
    }

    void StartRotationMenu()
    {
        Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    public float duration = 3f;

    IEnumerator ReinitializeCamera()
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, startRotationCamera, Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.rotation = startRotationCamera;

    }

    public void RestartLevel()
    {
        StartRotationMenu();
        LevelManager.manager.DestroyCurrentLevel();
        MenuManager.manager.StartMenu();
        mainMenuActive = true;
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
        
        if (mainMenuActive)
        {
            Camera.main.transform.Rotate(new Vector3(0, Time.deltaTime * speedCamera, 0));
        }
    }


}
