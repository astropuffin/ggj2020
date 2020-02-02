using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class catAnimations : MonoBehaviour
{
    public Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKey(KeyCode.B))
        {
            ani.SetFloat("howMuchHappy", Mathf.Lerp(ani.GetFloat("howMuchHappy"), 1, 5*Time.deltaTime));
        } 
       else
        {
            ani.SetFloat("howMuchHappy", Mathf.Lerp(ani.GetFloat("howMuchHappy"), 0, 5 * Time.deltaTime));
        }
    }
}
