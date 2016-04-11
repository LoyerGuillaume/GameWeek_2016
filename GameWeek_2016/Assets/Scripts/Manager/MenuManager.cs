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

    public void PlayClick()
    {
        DestroyCurrentMenu();
        GameManager.manager.StartLevel();
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
