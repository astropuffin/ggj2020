using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CatPetter : MonoBehaviour
{
    public float inputRegisterTime;
    public Dictionary<KeyCode, float> keyTimers = new Dictionary<KeyCode, float>();

    HashSet<KeyCode> leftRegion = new HashSet<KeyCode>()
    {
        KeyCode.A, KeyCode.S, KeyCode.D,
        KeyCode.Q, KeyCode.W, KeyCode.E,
        KeyCode.Z, KeyCode.X, KeyCode.C,
        KeyCode.CapsLock, KeyCode.Tab,
        KeyCode.LeftShift, KeyCode.LeftControl,
        KeyCode.LeftAlt, KeyCode.LeftWindows,
        KeyCode.Tilde, KeyCode.Alpha1, KeyCode.Alpha2,
        KeyCode.Alpha3,
    };

    HashSet<KeyCode> centerRegion = new HashSet<KeyCode>()
    {
        KeyCode.F, KeyCode.V, KeyCode.R, KeyCode.Alpha4,
        KeyCode.T, KeyCode.G, KeyCode.B, KeyCode.Alpha5, KeyCode.Alpha6,
        KeyCode.Space, KeyCode.Y, KeyCode.H, KeyCode.N,
        KeyCode.U, KeyCode.J, KeyCode.M, KeyCode.Alpha7,
        KeyCode.Alpha8, KeyCode.I, KeyCode.K, KeyCode.Comma
    };


    HashSet<KeyCode> rightRegion = new HashSet<KeyCode>()
    {
        KeyCode.Alpha9, KeyCode.O, KeyCode.L, KeyCode.Period, KeyCode.RightAlt,
        KeyCode.P, KeyCode.Semicolon, KeyCode.Slash, KeyCode.RightWindows,
        KeyCode.RightControl, KeyCode.Return, KeyCode.RightShift, KeyCode.Backslash,
        KeyCode.Backspace, KeyCode.Alpha0, KeyCode.Minus, KeyCode.Plus,
        KeyCode.P, KeyCode.Quote, KeyCode.LeftBracket, KeyCode.RightBracket,
    };


    public GameObject leftIndicator, rightIndicator, centerIndicator;
    public TextMeshPro leftText, centerText, rightText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    bool DetectKeyInRegion(HashSet<KeyCode> region)
    {
        bool pressed = false;
        foreach (var code in region)
        {
            if (Input.GetKey(code))
            {
                if(keyTimers.ContainsKey(code))
                {
                    keyTimers[code] = inputRegisterTime;
                }
                else
                {
                    keyTimers.Add(code, inputRegisterTime);
                }
                pressed = true;
            }
        }

        return pressed;
    }

    public bool left, right, center;
    HashSet<KeyCode> currentLeftRegionKeys = new HashSet<KeyCode>();
    HashSet<KeyCode> currentRightRegionKeys = new HashSet<KeyCode>();
    HashSet<KeyCode> currentCenterRegionKeys = new HashSet<KeyCode>();


    

    public enum PettingType
    {
        poke = 1,
        tickle = 2,
        scratch = 4,
        pet = 5,
    }

    void SetRegionText(int count, TextMeshPro textobj )
    {
        if(count == 1)
        {
            textobj.text = "poke";
        }
        if (count == 2)
        {
            textobj.text = "tickle";
        }
        if (count == 4)
        {
            textobj.text = "scratch";
        }

        if(count >= 5)
        {
            textobj.text = "pet";
        }

    }

    int CountKeysInRegion(HashSet<KeyCode> region)
    {
        int count = 0;

        foreach(var key in region)
        {
            if(keyTimers.ContainsKey(key))
            {
                var time = keyTimers[key];
                if( time > 0)
                {
                    count++;
                }

                keyTimers[key] -= Time.deltaTime;
            }
        }

        return count;
    }


    // Update is called once per frame
    void Update()
    {
        left = DetectKeyInRegion(leftRegion);
        right = DetectKeyInRegion(rightRegion);
        center = DetectKeyInRegion(centerRegion);

        leftIndicator.SetActive(left);
        rightIndicator.SetActive(right);
        centerIndicator.SetActive(center);

        int leftKeyCount = CountKeysInRegion(leftRegion);
        int rightKeyCount = CountKeysInRegion(rightRegion);
        int centerKeyCount = CountKeysInRegion(centerRegion);

        SetRegionText(leftKeyCount, leftText);
        SetRegionText(rightKeyCount, rightText);
        SetRegionText(centerKeyCount, centerText);

    }
}

