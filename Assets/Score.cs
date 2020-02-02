using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;
    int points;

    public void AddPoint()
    {
        points++;
        text.text = "Cat Pets: " + points.ToString();
    }


    // Start is called before the first frame update
    void Start()
    {
        text.text = "Cat Pets: " + points.ToString();
    }
}
