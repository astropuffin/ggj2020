using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Goal : MonoBehaviour
{
    public RectTransform bar;
    public RectTransform holder;
    public Image barImage;
    public float timeRemaining;
    public float maxTime;
    public float goalPosition, goalSize;
    public CatBar dad;
    public float timeToFill;
    public Color nice;
    public Color bad;
    public bool overlaps;
    public Gradient badToGood;

    public bool DetectOverlap(float pos)
    {
        return pos > goalPosition - goalSize / 2 && pos < goalPosition + goalSize / 2;
    }

    public void ShowPower(bool shake)
    {
        float normalizedPower =  timeRemaining / maxTime;
        bar.anchorMax = new Vector2(normalizedPower, bar.anchorMax.y);

        if(shake)
            holder.DOShakeScale(0.1f, normalizedPower / 20.0f);
    }

    bool once = true;


    // Update is called once per frame
    void Update()
    {
        overlaps = DetectOverlap(dad.indicatorPosition);

        if (overlaps)
        {
            // to fill in five seconds, we need to add 1/5th of maxTime per second
            timeRemaining += (maxTime / timeToFill) * Time.deltaTime;
            ShowPower(true);
        }
        else
        {
            timeRemaining -= Time.deltaTime;
            ShowPower(false);
        }

        if(timeRemaining > maxTime)
        {
            dad.ScorePoint(this);
        }

        if(timeRemaining < 0)
        {
            dad.FailPoint(this);
        }

        if(once && timeRemaining < 5)
        {
            //FindObjectOfType<AudioManager>().PlayTimeWarning();
            //once = false;
        }

        //barImage.color = Color.Lerp(bad, nice, timeRemaining / maxTime);
        barImage.color = badToGood.Evaluate(timeRemaining / (maxTime/1.33f));
    }
}
