using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionIcons : MonoBehaviour
{
    public float waitToBlank;
    public CatPetter catPetter;
    public Image leftPet;
    public Image middlePet;
    public Image rightPet;
    public Image leftScratch;
    public Image middleScratch;
    public Image rightScratch;

    float leftShowTime;
    float middleShowTime;
    float rightShowTime;
    // Start is called before the first frame update
    void Start()
    {
        leftPet.enabled = false;
        rightPet.enabled = false;
        middlePet.enabled = false;
        leftScratch.enabled = false;
        middleScratch.enabled = false;
        rightScratch.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLefts();
        UpdateMiddles();
        UpdateRights();
    }
    void UpdateLefts()
    {
        if (catPetter.currentLeftAction == Task.pet)
        {
            leftShowTime = Time.time;
            leftPet.enabled = true;
            leftScratch.enabled = false;
        }
        if (catPetter.currentLeftAction == Task.tickle)
        {
            leftShowTime = Time.time;
            leftPet.enabled = false;
            leftScratch.enabled = true;
        }
        if (catPetter.currentLeftAction == Task.none&&Time.time-leftShowTime> waitToBlank)
        {
            leftPet.enabled = false;
            leftScratch.enabled = false;
        }
    }

    void UpdateMiddles()
    {
        if (catPetter.currentCenterAction == Task.pet)
        {
            middleShowTime = Time.time;
            middlePet.enabled = true;
            middleScratch.enabled = false;
        }
        if (catPetter.currentCenterAction == Task.tickle)
        {
            middleShowTime = Time.time;
            middlePet.enabled = false;
            middleScratch.enabled = true;
        }
        if (catPetter.currentCenterAction == Task.none && Time.time - middleShowTime > waitToBlank)
        {
            middlePet.enabled = false;
            middleScratch.enabled = false;
        }
    }

    void UpdateRights()
    {
        if (catPetter.currentRightAction == Task.pet)
        {
            rightShowTime = Time.time;
            rightPet.enabled = true;
            rightScratch.enabled = false;
        }
        if (catPetter.currentRightAction == Task.tickle)
        {
            rightShowTime = Time.time;
            rightPet.enabled = false;
            rightScratch.enabled = true;
        }
        if (catPetter.currentRightAction == Task.none && Time.time - rightShowTime > waitToBlank)
        {
            rightPet.enabled = false;
            rightScratch.enabled = false;
        }
    }
}
