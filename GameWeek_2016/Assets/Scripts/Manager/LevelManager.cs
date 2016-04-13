using UnityEngine;
using System.Collections;

public class LevelManager : BaseManager<LevelManager>
{

    private const string PATH_LEVEL = "Levels/";

    public const string LEFT_HAND = "LEFT_HAND";
    public const string RIGHT_HAND = "RIGHT_HAND";

    public GameObject currentLevel;

    private int countBrokenAltar = 1;

    public int damagePerSeconds = 2;

    private int lifeHandRight;
    private int lifeHandLeft;

    public int score = 0;

    public bool leftHandAlive;
    public bool rightHandAlive;

    float elapsedTime = 0;

    private bool levelStart = false;

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
        levelStart = true;
    }

    private void InitLevelParam()
    {
        score = 0;
        lifeHandLeft = 100;
        lifeHandRight = 100;
        leftHandAlive = true;
        rightHandAlive = true;
    }

    public void DamageHand(string handType)
    {
        if (LevelManager.LEFT_HAND == handType && lifeHandLeft > 0)
        {
            lifeHandLeft -= damagePerSeconds;

            if (lifeHandLeft <= 0)
            {
                leftHandAlive = false;
            }
        }
        else if (LevelManager.RIGHT_HAND == handType && lifeHandRight > 0)
        {
            lifeHandRight -= damagePerSeconds;

            if (lifeHandRight <= 0)
            {
                rightHandAlive = false;
            }
        }
    }

    public void AltarBroken()
    {
        countBrokenAltar++;
    }

    public void AltarRegen()
    {
        countBrokenAltar--;
    }

    public void DamageCurrentHand()
    {
        HandController handController = currentLevel.transform.FindChild("HandController").GetComponent<HandController>();
        DamageHand(handController.GetTypeHandActive());
    }
    
    void Update ()
    {
        if (!levelStart)
        {
            return;
        }

        elapsedTime += Time.deltaTime;
        
        if (countBrokenAltar > 0 && elapsedTime >= 1)
        {
            UpdatePerSeconde();
            elapsedTime = elapsedTime % 1;
        }

        if (!leftHandAlive && !rightHandAlive)
        {
            print("LevelManager - GameOver");
            levelStart = false;
            GameManager.manager.StartGameOver();
        }

        print("Left Hand Life : " + lifeHandLeft);
        print("Left Hand Alive : " + leftHandAlive);
        print("Right Hand Life : " + lifeHandRight);
        print("Right Hand Alive : " + rightHandAlive);
    }

    void UpdatePerSeconde()
    {
        score++;
        DamageCurrentHand();
    }

    public void DestroyCurrentLevel()
    {
        GameObject.Destroy(currentLevel);
    }


}
