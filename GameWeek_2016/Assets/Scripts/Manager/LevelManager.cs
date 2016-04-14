using UnityEngine;
using System.Collections;

public class LevelManager : BaseManager<LevelManager>
{

    private const string PATH_LEVEL = "Levels/";

    public const string LEFT_HAND = "LEFT_HAND";
    public const string RIGHT_HAND = "RIGHT_HAND";

    public GameObject currentLevel;

    private int countBrokenAltar = 0;

    public int damagePerSeconds = 2;

    private int lifeHandRight;
    private int lifeHandLeft;

    public int score = 0;

    public bool leftHandAlive;
    public bool rightHandAlive;

    public Material handMat = null;

    float elapsedTime = 0;

    public bool levelStart = false;

    protected override IEnumerator CoroutineStart()
    {
        Color handColor = handMat.color;
        handMat.color = new Color(handColor.r, handColor.g, handColor.b, 1);

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
                GameManager.manager.HandDead();
            }
        }
        else if (LevelManager.RIGHT_HAND == handType && lifeHandRight > 0)
        {
            lifeHandRight -= damagePerSeconds;

            if (lifeHandRight <= 0)
            {
                rightHandAlive = false;
                GameManager.manager.HandDead();
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
        
        if (elapsedTime >= 1)
        {
            UpdatePerSeconde();
            elapsedTime = elapsedTime % 1;
        }

        Color handColor = handMat.color;
        float handLife = (GameObject.Find("HandController").GetComponent<HandController>().GetTypeHandActive() == "LEFT_HAND") ? lifeHandLeft : lifeHandRight;
        float alpha = handLife / 100f;
        handMat.color = new Color(handColor.r, handColor.g, handColor.b, alpha);


        //if (!leftHandAlive && !rightHandAlive)
        //{
        //    print("LevelManager - GameOver");
        //    levelStart = false;
        //    GameManager.manager.StartGameOver();
        //}

        //print("Left Hand Life : " + lifeHandLeft);
        //print("Left Hand Alive : " + leftHandAlive);
        //print("Right Hand Life : " + lifeHandRight);
        //print("Right Hand Alive : " + rightHandAlive);
    }



    public void ChangeHand ()
    {
        if ((!leftHandAlive && rightHandAlive) || (leftHandAlive && !rightHandAlive))
        {
            GameManager.manager.RemoveInfoHand();
        }
    }

    void UpdatePerSeconde()
    {
        score++;
        if (countBrokenAltar > 0)
        {
            DamageCurrentHand();
        }
    }

    public void DestroyCurrentLevel()
    {
        GameObject.Find("HandController").GetComponent<HandController>().DestroyAllHands();
        GameObject.Find("GeneratorManager").GetComponent<GeneratorManager>().StopCoroutineWave();
        GameObject.Destroy(currentLevel);
    }


}
