using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatLord : MonoBehaviour
{
    public Animator ani;


    // Update is called once per frame
    void Update()
    {
        var goals = FindObjectsOfType<Goal>();
        foreach(var g in goals)
        {
            if(g.overlaps)
            {
                ani.SetFloat("howMuchHappy", Mathf.Lerp(ani.GetFloat("howMuchHappy"), 1, 9 * Time.deltaTime));
                return;
            }
        }

        ani.SetFloat("howMuchHappy", Mathf.Lerp(ani.GetFloat("howMuchHappy"), 0, 9 * Time.deltaTime));
    }
}
