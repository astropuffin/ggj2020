using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    bool once = true;

    // Update is called once per frame
    void Update()
    {
        text.text = "<mspace=0.8em>" + timer.ToString("F1");
        timer -= Time.deltaTime;
        text.color = badToGood.Evaluate(Mathf.Clamp01(timer / timerStartVal));

        if(timer < 5 && once)
        {
            GameObject.Find("AudioManager").GetComponent<AudioManager>().PlayTimeWarning();
            once = false;
        }

        if(timer < 0)
        {
            GameObject.Find("AudioManager").GetComponent<AudioManager>().StopTimeWarning();
            SceneManager.LoadScene("menu");
        }
    }
}
