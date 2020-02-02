﻿using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
    public Sound[] MeowSounds;
    private int meowIndex = 0;
    public Sound[] PurrSounds;
    private int purrIndex = 0;
    public Sound[] AngrySounds;
    private int angreIndex = 0;
    public Sound BackgroundMusic;

    public static AudioManager instance;
    void Awake()
    {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        foreach (Sound s in MeowSounds) {
            InitSound(s);
        }
        foreach (Sound s in PurrSounds) {
            InitSound(s);
        }
        foreach (Sound s in AngrySounds) {
            InitSound(s);
        }
        InitSound(BackgroundMusic);
    }

    private void Start() {
        PlayBackgroundMusic();
    }

    private void InitSound(Sound s) {
        s.source = gameObject.AddComponent<AudioSource>();
        s.source.clip = s.clip;
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.loop = s.loop;
    }

    public void PlayMeow() {
        PlaySoundArray(MeowSounds, meowIndex);
    }

    public void PlayPurr() {
        PlaySoundArray(PurrSounds, purrIndex);
    }

    public void PlayAngry() {
        PlaySoundArray(AngrySounds, angreIndex);
    }

    private void PlaySoundArray(Sound[] array, int index) {
        array[index].source.Play();
        index++;
        index %= array.Length;
    }

    public void StopAngry() {
        StopSound(AngrySounds, angreIndex);
    }

    public void StopPurr() {
        StopSound(PurrSounds, purrIndex);
    }

    public void StopMeow() {
        StopSound(MeowSounds, meowIndex);
    }

    public void StopSound(Sound[] array, int index) {
        array[index].source.Stop();
    }

    public void PlayBackgroundMusic() {
        BackgroundMusic.source.Play();
    }
}
