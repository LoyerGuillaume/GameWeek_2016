using UnityEngine;
using System.Collections;

public class LevelManager : BaseManager<LevelManager>
{

    private const string PATH_LEVEL = "Levels/";

    public const string LEFT_HAND = "LEFT_HAND";
    public const string RIGHT_HAND = "RIGHT_HAND";

    public GameObject currentLevel;

    public int damagePerSeconds = 2;

    private int lifeHandRight = 100;
    private int lifeHandLeft = 100;

    protected override IEnumerator CoroutineStart()
    {
        IsReady = true;
        yield return null;
    }

    public void StartLevel()
    {
        print("LevelManager - StartLevel");
        currentLevel = Instantiate(Resources.Load(PATH_LEVEL + "LevelLeapMotion")) as GameObject;
        InitLevelParam();
    }

    private void InitLevelParam()
    {
        lifeHandLeft = 100;
        lifeHandRight = 100;
    }

    public void DamageHand(string handType)
    {
        if (LevelManager.LEFT_HAND == handType)
        {
            lifeHandLeft -= damagePerSeconds;
        }
        else if (LevelManager.RIGHT_HAND == handType)
        {
            lifeHandRight -= damagePerSeconds;
        }
    }

    public void AltarWither()
    {
        HandController handController = currentLevel.transform.FindChild("HandController").GetComponent<HandController>();
        DamageHand(handController.GetTypeHandActive());
    }


}
