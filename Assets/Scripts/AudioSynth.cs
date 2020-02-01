using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSynth : MonoBehaviour {
    public double frequency = 440;
    private double _increment;
    private double _phase;
    private readonly double _samplingFrequency = 48000.0;

    public float gain;
    public float volume = 0.1f;
    public float twelveToneRatio = Mathf.Pow(2,1f/12f);

    public double[] frequencies;
    public int thisFreq;
    public WaveType waveType;

    public enum WaveType {
        Sin,
        Square,
        Triangle
    }

    void Start() {
        gain = 0;
        waveType = WaveType.Sin;
        frequencies = new [] {
            frequency,
            frequency * Mathf.Pow(twelveToneRatio, 1),
            frequency * Mathf.Pow(twelveToneRatio, 2),
            frequency * Mathf.Pow(twelveToneRatio, 3),
            frequency * Mathf.Pow(twelveToneRatio, 4),
            frequency * Mathf.Pow(twelveToneRatio, 5),
            frequency * Mathf.Pow(twelveToneRatio, 6),
            frequency * Mathf.Pow(twelveToneRatio, 7),
            frequency * Mathf.Pow(twelveToneRatio, 8),
            frequency * Mathf.Pow(twelveToneRatio, 9),
            frequency * Mathf.Pow(twelveToneRatio, 10),
            frequency * Mathf.Pow(twelveToneRatio, 11),
            frequency * Mathf.Pow(twelveToneRatio, 12),
        };
        thisFreq = 0;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            gain = volume;
            frequency = frequencies[thisFreq];
            thisFreq += 1;
            thisFreq %= frequencies.Length;
        }
    }

    private void OnAudioFilterRead(float[] data, int channels) {
        _increment = frequency * 2.0 * Mathf.PI / _samplingFrequency;

        for (int i = 0; i < data.Length; i += channels) {
            _phase += _increment;
            data[i] = gain * Wave(_phase);

            if (channels == 2) {
                data[i + 1] = data[i];
            }

            if (_phase > (Mathf.PI * 2)) {
                _phase = 0.0f;
            }
        }
    }

    private float Wave(double phase) {
        switch (waveType) {
            case WaveType.Sin:
                return SinWave(phase);
            case WaveType.Triangle:
                return TriangleWave(phase);
            case WaveType.Square:
                return SquareWave(phase);
            default:
                return 0f;
        }
    }

    private static float SinWave(double phase) {
        return Mathf.Sin((float) phase);
    }

    private static float SquareWave(double phase) {
        if (Mathf.Sin((float) phase) >= 0) {
            return 0.6f;
        }
        return -0.6f;
    }

    private static float TriangleWave(double phase) { return Mathf.PingPong((float) phase, 1.0f); }
}
