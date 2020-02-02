using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tolerance : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;
    public float timePerPoint;
    public float timer;
    public float timerStartVal;
    public Gradient badToGood;
    public void AddPoint()
    {
        timer += timePerPoint;
    }
    void Start()
    {
        timer = timerStartVal;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Tolerance Exhausted in: " + timer.ToString("F1");
        timer -= Time.deltaTime;
        text.color = badToGood.Evaluate(Mathf.Clamp01(timer / timerStartVal));
    }
}
