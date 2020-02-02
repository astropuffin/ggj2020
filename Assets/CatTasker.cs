using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Task
{
    poke,
    tickle,
    pet,
    none
}

public enum Region
{
    left,
    center,
    right
}

public class CatTasker : MonoBehaviour
{
    public Text taskText;
    public RectTransform bar;
    public CatPetter petter;


    public Task currentTask;
    public Region currentRegion;
    public float decaySpeed;
    public float startingPower;
    public float powerPerSecond;
    public float currentPurrPower;


    public float currentVel;
    public float acceleration;
    public float maxVelocity;
    public float gravity;
    public float drag;

    public void SetPurrPower(float purrPower)
    {
        bar.anchorMax = new Vector2(purrPower, bar.anchorMax.y);
    }


    // Start is called before the first frame update
    void Start()
    {
        RandomizeTask();
    }

    public void RandomizeTask()
    {
        currentTask = (Task)Random.Range(0, 3);
        currentRegion = (Region)Random.Range(0, 3);
        taskText.text = currentTask.ToString() + " " + currentRegion.ToString();
        currentPurrPower = startingPower;
    }

    // Update is called once per frame
    void Update()
    {
        var purrPowerIncreasing = petter.IsPettingRegion(currentTask, currentRegion);
        if(purrPowerIncreasing)
        {
            currentVel += acceleration * Time.deltaTime;

        }
        currentVel -= gravity * Time.deltaTime;

        currentVel = Mathf.Clamp(currentVel, -maxVelocity, maxVelocity);
        currentPurrPower += currentVel * Time.deltaTime;

        
        currentPurrPower = Mathf.Clamp01(currentPurrPower);
        if (currentPurrPower == 0 || currentPurrPower == 1)
        {
            currentVel = 0;
        }
        
    }
}
