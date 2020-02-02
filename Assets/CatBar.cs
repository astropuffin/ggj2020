using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBar : MonoBehaviour
{
    public float goalSize;
    public float goalPosition;

    public float indicatorPosition;
    public float acceleration;
    public float drag;
    public float maxVelocity;
    public float currentVel;

    public RectTransform goal, indicator, master;
    public Region region;
    public CatPetter petter;

    public float powerSpeed, powerDecay, purrPower;
    public RectTransform powerBar;

    // Start is called before the first frame update
    void Start()
    {
        RandomizeGoal();
    }

    void SetIndicatorPos(float pos)
    {
        var height = master.sizeDelta.y;
        var relativePos = height * pos;
        indicator.anchoredPosition = new Vector2(indicator.anchoredPosition.x, relativePos);
    }

    bool DetectOverlap()
    {
        return indicatorPosition > goalPosition - goalSize / 2 && indicatorPosition < goalPosition + goalSize / 2;
    }

    void RandomizeGoal()
    {
        var masterHeight = master.sizeDelta.y;

        goalSize = Random.Range(0.03f, 0.1f);
        goalPosition = Random.value;

        var relativeHeight = goalPosition * masterHeight;

        goal.anchoredPosition = new Vector2(goal.anchoredPosition.x, relativeHeight);
        goal.sizeDelta = new Vector2(goal.sizeDelta.x, goalSize * masterHeight);

    }

    void ShowPower()
    {
        powerBar.anchorMax = new Vector2(purrPower, powerBar.anchorMax.y);

    }


    // Update is called once per frame
    void Update()
    {
        var scratch = petter.IsPettingRegion(Task.tickle, region);
        var pet = petter.IsPettingRegion(Task.pet, region);

        if (scratch)
        {
            currentVel -= acceleration * Time.deltaTime;
        }

        if (pet)
        {
            currentVel += acceleration * Time.deltaTime;
        }

        currentVel = Mathf.Clamp(currentVel, -maxVelocity, maxVelocity);
        indicatorPosition += currentVel * Time.deltaTime;
        indicatorPosition = Mathf.Clamp01(indicatorPosition);

        if (indicatorPosition == 0 || indicatorPosition == 1)
        {
            currentVel = 0;
        }

        currentVel *= drag;
        SetIndicatorPos(indicatorPosition);

        if(DetectOverlap())
        {
            purrPower += powerSpeed * Time.deltaTime;
        }
        else
        {
            purrPower -= powerDecay * Time.deltaTime;
        }

        if (purrPower > 1)
            RandomizeGoal();

        purrPower = Mathf.Clamp01(purrPower);

        

        ShowPower();

    }
}
