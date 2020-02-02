using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tolerance : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;
    public float timePerPoint;
    public float timer;

    public void AddPoint()
    {
        timer += timePerPoint;
    }


    // Update is called once per frame
    void Update()
    {
        text.text = "Tolerance Exhausted in: " + timer.ToString("F1");
        timer -= Time.deltaTime;
    }
}
