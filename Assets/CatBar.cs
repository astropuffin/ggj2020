using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CatBar : MonoBehaviour
{
    public float goalSize;
    public float goalPosition;

    public float indicatorPosition;
    public float accelerationPet, accelerationTickle;
    public float drag;
    public float maxVelocity;
    public float currentVel;

    public RectTransform indicator, master;
    public Region region;
    public CatPetter petter;

    public float powerSpeed, powerDecay, purrPower;
    public GameObject goalPrefab;


    public AudioManager audioManager;
    public float difficulty;

    public float spawnTimer;

    static int numberOfHappyCats;
    public Animator ani;
    public bool overlapping;

    void SpawnGoal()
    {
        var canvas = transform.parent;
        var goal = Instantiate(goalPrefab).GetComponent<Goal>();


        var masterHeight = master.sizeDelta.y;

        goalSize = Random.Range(0.05f, 0.1f);
        goalPosition = Random.value;
        var relativeHeight = goalPosition * masterHeight;
        goal.transform.SetParent(transform);
        goal.transform.localScale = Vector3.one;
        var gr = goal.GetComponent<RectTransform>();

        gr.anchoredPosition = new Vector2(0, relativeHeight);
        gr.sizeDelta = new Vector2(gr.sizeDelta.x, goalSize * masterHeight);

        goal.timeToFill = 5;
        goal.timeRemaining = 10;
        goal.maxTime = 20;
        goal.goalPosition = goalPosition;
        goal.goalSize = goalSize;
        goal.dad = this;
        audioManager.PlayPop();
    }

    void SetIndicatorPos(float pos)
    {
        var height = master.sizeDelta.y;
        var relativePos = height * pos;
        indicator.anchoredPosition = new Vector2(indicator.anchoredPosition.x, relativePos);
    }

    private void Start()
    {
        spawnTimer = Random.Range(1.0f, 2.0f);
    }


    // Update is called once per frame
    void Update()
    {
        var scratch = petter.IsPettingRegion(Task.tickle, region);
        var pet = petter.IsPettingRegion(Task.pet, region);

        if (scratch)
        {
            currentVel -= accelerationTickle;
            petter.ClearInputs(region);
        }

        if (pet)
        {
            currentVel += accelerationPet;
            petter.ClearInputs(region);
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

        if(spawnTimer < 0)
        {
            SpawnGoal();
            spawnTimer = Random.Range(5.0f, difficulty);
        }

        spawnTimer -= Time.deltaTime;


        if(overlapping)
        {
          ani.SetFloat("howMuchHappy", Mathf.Lerp(ani.GetFloat("howMuchHappy"), 1, 9 * Time.deltaTime));
        }
        else
        {
          ani.SetFloat("howMuchHappy", Mathf.Lerp(ani.GetFloat("howMuchHappy"), 0, 9 * Time.deltaTime));
        }
    }

    public void ScorePoint(Goal g)
    {
        FindObjectOfType<Tolerance>().AddPoint();
        FindObjectOfType<Score>().AddPoint();
        Destroy(g.gameObject);
        audioManager.PlayMeow();
    }

    public void FailPoint(Goal g)
    {
        Destroy(g.gameObject);
        //audioManager.PlayAngry();
    }

}
