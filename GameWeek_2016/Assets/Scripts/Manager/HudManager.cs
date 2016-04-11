using UnityEngine;
using System.Collections;

public class HudManager : MonoBehaviour {
    
    private const string PATH_UI = "UI/";
    private GameObject currentHud;


    public void StartHud()
    {
        EnabledHud();
    }

    public void InitHud()
    {
        currentHud = Instantiate(Resources.Load(PATH_UI + "HUD")) as GameObject;
        DisabledHud();
    }


    public void DisabledHud()
    {
        currentHud.SetActive(false);
    }
    
    public void EnabledHud()
    {
        currentHud.SetActive(true);
    }
}
