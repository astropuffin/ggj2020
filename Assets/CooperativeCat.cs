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
        var proportion = masterWidth / windowWidth;
        var farExtent = masterWidth - windowWidth;
        var xPos = Mathf.Lerp(0, farExtent, power);

        window.anchoredPosition = new Vector2(xPos, window.anchoredPosition.y);
    }

    void SetSinkerPos(float power)
    {

    }


    // Update is called once per frame
    void Update()
    {
        SetWindowPos(ct1.currentPurrPower);



    }
}
