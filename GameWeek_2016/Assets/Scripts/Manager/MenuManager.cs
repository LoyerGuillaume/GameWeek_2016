using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : BaseManager<MenuManager> {


    private const string PATH_UI = "UI/";

    private GameObject currentMenu;

    protected override IEnumerator CoroutineStart()
    {
        IsReady = true;
        yield return null;
    }

    public void StartMenu ()
    {
        currentMenu = Instantiate(Resources.Load(PATH_UI + "MainMenu")) as GameObject;
        currentMenu.SetActive(true);

        GameObject btnPlay = GameObject.Find("Play");
        btnPlay.GetComponent<Button>().onClick.AddListener(PlayClick);
    }

    public void StartGameOverMenu()
    {
        currentMenu = Instantiate(Resources.Load(PATH_UI + "GameOverMenu")) as GameObject;
        currentMenu.SetActive(true);

        GameObject ScoreGameObject = GameObject.Find("ScoreText").gameObject;
        Text ScoreText = ScoreGameObject.GetComponent<Text>();
        ScoreText.text = "Score : " + LevelManager.manager.score;
        
        GameObject btnRetry = GameObject.Find("RestartButton");
        btnRetry.GetComponent<Button>().onClick.AddListener(RestartClick);

    }

    public void PlayClick()
    {
        DestroyCurrentMenu();
        GameManager.manager.StartLevel();
    }

    public void RestartClick()
    {
        DestroyCurrentMenu();
        GameManager.manager.RestartLevel();
    }

    public void DestroyCurrentMenu()
    {
        currentMenu.SetActive(false);
        Destroy(currentMenu);
        currentMenu = null;
    }


    // Update is called once per frame
    void Update () {
	
	}
}
