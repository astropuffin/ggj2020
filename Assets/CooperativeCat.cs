using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Player
{
    one,
    two
}

public class CooperativeCat : MonoBehaviour
{
    public Player windowPlayer = Player.one;
    const Player one = Player.one;
    const Player two = Player.two;

    public float windowWidth;
    public CatTasker ct1, ct2;
    public RectTransform window, sinker;
    public RectTransform masterWindow;

    public float sinkerPower, sinkerDecay;
    public float windowPower, windowDecay;
    public float pleasurePower, pleasureDecay;
    public float currentPleasure;
    public RectTransform pleasureBar;

    Dictionary<Player, PlayerState> state = new Dictionary<Player, PlayerState>();


    struct PlayerState
    {
        public RectTransform target;
        public float power, decay;
    }



    void SetTaskerStats(CatTasker ct, PlayerState ps)
    {
        
        ct.decaySpeed = ps.decay;
        ct.powerPerSecond = ps.power;
    }

    // Start is called before the first frame update
    void Start()
    {
        masterWindow.sizeDelta = new Vector2(Screen.width, masterWindow.sizeDelta.y);
        window.sizeDelta = new Vector2(masterWindow.sizeDelta.x * windowWidth, window.sizeDelta.y);

        PlayerState windowState = new PlayerState();
        windowState.decay = windowDecay;
        windowState.power = windowPower;
        windowState.target = window;

        PlayerState sinkerState = new PlayerState();
        sinkerState.decay = sinkerDecay;
        sinkerState.power = sinkerPower;
        sinkerState.target = sinker;

        state.Add(one, windowState);
        state.Add(two, sinkerState);

        SetTaskerStats(ct1, windowState);
        SetTaskerStats(ct2, sinkerState);
    }

    void SetWindowPos(float power)
    {
        var masterWidth = masterWindow.sizeDelta.x;
        var windowWidth = window.sizeDelta.x;
        var farExtent = masterWidth - windowWidth;
        var xPos = Mathf.Lerp(0, farExtent, power);

        window.anchoredPosition = new Vector2(xPos, window.anchoredPosition.y);
    }

    void SetSinkerPos(float power)
    {
        var masterWidth = masterWindow.sizeDelta.x;
        var sinkerWidth = sinker.sizeDelta.x;
        var farExtent = masterWidth - sinkerWidth;
        var xPos = Mathf.Lerp(0, -farExtent, power);

        sinker.anchoredPosition = new Vector2(xPos, sinker.anchoredPosition.y);
    }

    bool DetectOverlap()
    {
        var masterWidth = masterWindow.sizeDelta.x;
        var windowWideth = window.sizeDelta.x;
        var sinkerRelativePosition = masterWidth + sinker.anchoredPosition.x;
        sinkerRelativePosition = window.anchoredPosition.x - sinkerRelativePosition;
        sinkerRelativePosition *= -1;

        if (sinkerRelativePosition <= windowWideth && sinkerRelativePosition >= 0)
            return true;

        return false;
    }

    public void SetPurrPower(float purrPower)
    {
        pleasureBar.anchorMax = new Vector2(purrPower, pleasureBar.anchorMax.y);
    }


    void RandomizeTask()
    {
        ct1.RandomizeTask();
        ct2.RandomizeTask();
        while(ct1.currentRegion == ct2.currentRegion)
        {
            ct1.RandomizeTask();
            ct2.RandomizeTask();
        }

        currentPleasure = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        SetWindowPos(ct1.currentPurrPower);
        SetSinkerPos(ct2.currentPurrPower);

        if(DetectOverlap())
        {
            currentPleasure += pleasurePower * Time.deltaTime;
        }
        else
        {
            currentPleasure -= pleasurePower * Time.deltaTime;
        }

        if (currentPleasure >= 1)
            RandomizeTask();

        currentPleasure = Mathf.Clamp01(currentPleasure);
        SetPurrPower(currentPleasure);

        
    }
}
