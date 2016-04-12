using UnityEngine;
using System.Collections;

public class CustomTimer : BaseManager<CustomTimer>
{
	public bool IsRunning{get;private set;}

	float _elapsedTime=0;

    private float timeRatio = 1f;

    public void ChangeTimeRatio (float ratio)
    {
        timeRatio = ratio;
    }

	public float DeltaTime
	{
		get{

			return IsRunning?Time.deltaTime * timeRatio:0;
		}
	}

	public float FixedDeltaTime
	{
		get{
			
			return IsRunning?Time.fixedDeltaTime * timeRatio:0;
		}
	}


	public float ElapsedTime
	{
		get{
			return _elapsedTime;
		}
		private set{
			_elapsedTime = value;
		}
	}

	public void StopTimer()
	{
		IsRunning = false;
	}

	public void StartTimer()
	{
		IsRunning = true;
	}

	public void Reset(bool startTimer)
	{
		ElapsedTime = 0;
		IsRunning = startTimer;
	}

	public void ResetAndStart()
	{
		Reset (true);
	}

	public void ResetAndStop()
	{
		Reset (false);
	}
	
	// Update is called once per frame
	void Update () {
        ElapsedTime += DeltaTime;
	}

    #region BaseManager Overriden Methods
    protected override IEnumerator CoroutineStart()
    {
        IsReady = true;
        yield break;
    }
    #endregion
}
