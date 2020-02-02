using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CatPetter : MonoBehaviour
{
    public float ppsWindow;
    public float inputRegisterTime;
    public Dictionary<KeyCode, float> keyTimers = new Dictionary<KeyCode, float>();
    public float leftPPS, centerPPS, rightPPS;
    public List<float> leftPets = new List<float>();
    public List<float> rightPets = new List<float>();
    public List<float> centerPets = new List<float>();

    public float leftTimer, centerTimer, rightTimer;
    public HashSet<KeyCode> leftKeysPressed = new HashSet<KeyCode>();
    public HashSet<KeyCode> rightKeysPressed = new HashSet<KeyCode>();
    public HashSet<KeyCode> centerKeysPressed = new HashSet<KeyCode>();


    public void ClearInputs(Region r)
    {
        keyTimers.Clear();

        if(r == Region.center)
        {
            centerKeysPressed.Clear();
        }
        else if(r == Region.left)
        {
            leftKeysPressed.Clear();
        }
        else if(r == Region.right)
        {
            rightKeysPressed.Clear();
        }
    }

    HashSet<KeyCode> leftRegion = new HashSet<KeyCode>()
    {
        KeyCode.A, KeyCode.S, KeyCode.D,
        KeyCode.Q, KeyCode.W, KeyCode.E,
        KeyCode.Z, KeyCode.X, KeyCode.C,
        KeyCode.CapsLock, KeyCode.Tab,
        KeyCode.LeftShift, KeyCode.LeftControl,
        KeyCode.LeftAlt, KeyCode.LeftWindows,
        KeyCode.Tilde, KeyCode.Alpha1, KeyCode.Alpha2,
        KeyCode.Alpha3, KeyCode.BackQuote
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

    bool DetectKeyInRegion(HashSet<KeyCode> region, ref float timer, HashSet<KeyCode> keysPressed)
    {
        bool pressed = false;
        foreach (var code in region)
        {
            if (Input.GetKeyDown(code))
            {
                if(keyTimers.ContainsKey(code))
                {
                    keyTimers[code] = inputRegisterTime;
                }
                else
                {
                    keyTimers.Add(code, inputRegisterTime);
                }

                if (timer <= 0)
                {
                    timer = inputRegisterTime;
                    keysPressed.Clear();
                }

                keysPressed.Add(code);

                pressed = true;
            }
        }

        return pressed;
    }

    public bool left, right, center;
    HashSet<KeyCode> currentLeftRegionKeys = new HashSet<KeyCode>();
    HashSet<KeyCode> currentRightRegionKeys = new HashSet<KeyCode>();
    HashSet<KeyCode> currentCenterRegionKeys = new HashSet<KeyCode>();


    public Task currentLeftAction, currentRightAction, currentCenterAction;

    

    void SetRegionText(int count, TextMeshPro textobj, ref Task task, List<float> petList )
    {
        if(count == 1)
        {
            textobj.text = "poke";
            task = Task.poke;
        }
        else if (count >= 3 && count <= 4)
        {
            textobj.text = "tickle";
            task = Task.tickle;
        }
        else if(count >= 5)
        {
            textobj.text = "pet";
            task = Task.pet;
            petList.Add(Time.realtimeSinceStartup);
        }
        else
        {
            textobj.text = "none";
            task = Task.none;
        }
    }

    int CountKeysInRegion(HashSet<KeyCode> keysPressed, float timer)
    {
        if (timer > 0)
            return 0;
        else
            return keysPressed.Count;
        /*
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
        */
    }

    public bool IsPettingRegion(Task task, Region region)
    {
        if(region == Region.left)
            return currentLeftAction == task;

        if (region == Region.right)
            return currentRightAction == task;

        if (region == Region.center)
            return currentCenterAction == task;


        return false;
    }

    float CalculatePPS(List<float> list)
    {
        var currentTime = Time.realtimeSinceStartup;

        for (int i = 0; i < list.Count; i++)
        {
            var reverseIndex = (list.Count - i - 1);
            if (currentTime - list[reverseIndex] > ppsWindow)
            {
                list.RemoveAt(reverseIndex);
            }
        }

        return list.Count / ppsWindow;
    }


    // Update is called once per frame
    void Update()
    {
        left = DetectKeyInRegion(leftRegion, ref leftTimer, leftKeysPressed);
        right = DetectKeyInRegion(rightRegion, ref rightTimer, rightKeysPressed);
        center = DetectKeyInRegion(centerRegion, ref centerTimer, centerKeysPressed);

        int leftKeyCount = CountKeysInRegion(leftKeysPressed, leftTimer);
        int rightKeyCount = CountKeysInRegion(rightKeysPressed, rightTimer);
        int centerKeyCount = CountKeysInRegion(centerKeysPressed, centerTimer);

        SetRegionText(leftKeyCount, leftText, ref currentLeftAction, leftPets);
        SetRegionText(rightKeyCount, rightText, ref currentRightAction, centerPets);
        SetRegionText(centerKeyCount, centerText, ref currentCenterAction, rightPets);

        leftPPS = CalculatePPS(leftPets);
        rightPPS = CalculatePPS(rightPets);
        centerPPS = CalculatePPS(centerPets);

        leftTimer -= Time.deltaTime;
        rightTimer -= Time.deltaTime;
        centerTimer -= Time.deltaTime;
    }
}

