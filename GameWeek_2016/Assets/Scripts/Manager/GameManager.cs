using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    
    public static GameManager manager;

    private const string PATH_LEVEL = "Levels/";

    bool infoMenuActive = false;

    public bool mainMenuActive = false;

    public bool gameOver = false;

    GameObject currentDemoMenu;

    GameObject settingTemple;

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

        PlayLoopMusic();

        startRotationCamera = Camera.main.transform.rotation;

        settingTemple = GameObject.Find("SettingTemple");

        MenuManager.manager.StartMenu();
        StartDemoMenu();
        StartRotationMenu();
        mainMenuActive = true;
    }

    private void PlayLoopMusic()
    {
        if (MusicLoopsManager.manager) MusicLoopsManager.manager.PlayMusic(MusicType.loopMusic);
    }

    public void StartLevel()
    {
        MenuManager.manager.DestroyCurrentMenu();
        DestroyMenuDemo();
        gameOver = false;
        print("GameManager - StartLevel");
        mainMenuActive = false;
        StartCoroutine(ReinitializeCamera());
        ReinitializeCamera();
        LevelManager.manager.StartLevel();
    }


    void StartRotationMenu()
    {

        //Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
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
    
    void StartDemoMenu()
    {
        currentDemoMenu = Instantiate(Resources.Load(PATH_LEVEL + "MenuDemo")) as GameObject;
        currentDemoMenu.SetActive(true);
    }

    public void RestartLevel()
    {
        LevelManager.manager.RestartLiveHand();
        LevelManager.manager.RestartAlphaHand();
        StartDemoMenu();
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


    void DestroyMenuDemo()
    {
        GameObject.Find("HandController").GetComponent<HandController>().DestroyAllHands();
        GameObject.Find("GeneratorManager").GetComponent<GeneratorManager>().StopCoroutineWave();
        currentDemoMenu.SetActive(false);
        GameObject.Destroy(currentDemoMenu);
    }


    // Update is called once per frame
    void Update () {
        
        if (mainMenuActive)
        {
            Vector3 rotateCamera = new Vector3(0, Time.deltaTime * speedCamera, 0);

            settingTemple.transform.Rotate(rotateCamera);
            //currentDemoMenu.transform.Rotate(rotateCamera);
            //Camera.main.transform.Rotate(rotateCamera);
        }
    }


}
