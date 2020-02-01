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

    public void SetPurrPower(float purrPower)
    {
        bar.anchorMax = new Vector2(purrPower, bar.anchorMax.y);
    }


    // Start is called before the first frame update
    void Start()
    {
        RandomizeTask();
    }

    void RandomizeTask()
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
            currentPurrPower += powerPerSecond * Time.deltaTime;
        }
        else
        {
            currentPurrPower -= decaySpeed * Time.deltaTime;
        }

        if(currentPurrPower >= 1.0f)
        {
            RandomizeTask();
        }

        currentPurrPower = Mathf.Clamp01(currentPurrPower);

        SetPurrPower(currentPurrPower);
    }
}
