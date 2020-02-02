﻿using System;
using UnityEngine.Audio;
using UnityEngine;

[Serializable]
public class Sound {
    public string name;
    public AudioClip clip;
    [Range(0f, 4f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;

    public bool loop;
}
