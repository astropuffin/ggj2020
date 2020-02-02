using UnityEngine;

public class AudioSynth : MonoBehaviour {
    public double frequency = 440f / 2f;
    public float gain;
    public float volume = 0.1f;
    public WaveType waveType;

    private double _increment;
    private double _phase;
    private readonly double _samplingFrequency = 48000.0;
    private readonly float _twelveToneRatio = Mathf.Pow(2,1f/12f);
    private float _keyDown;

    public enum WaveType {
        Sin,
        Square,
        Triangle,
        Cube,
        Soft,
        DoubleSoft,
        TripleSoft,
        Sawtooth,
        ReverseSawtooth
    }

    void Start() {
        gain = 0;
        waveType = WaveType.Sin;
        _keyDown = 0;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            _keyDown = Time.time;
            frequency *= _twelveToneRatio;
        }

        gain = volume;
        //gain = volume * Attenuation(Time.time - _keyDown);
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
            case WaveType.Cube:
                return CubeWave(phase);
            case WaveType.Soft:
                return SoftWave(phase);
            case WaveType.DoubleSoft:
                return DoubleSoftWave(phase);
            case WaveType.TripleSoft:
                return TripleSoftWave(phase);
            case WaveType.Sawtooth:
                return SawtoothWave(phase);
            case WaveType.ReverseSawtooth:
                return ReverseSawtoothWave(phase);
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

    private static float CubeWave(double phase) {
        var sin = Mathf.Sin((float) phase);
        return sin + sin * sin * sin;
    }

    private static float SoftWave(double phase) {
        return 0.6f * Mathf.Sin((float) phase)
                    + 0.4f * Mathf.Sin(0.5f * (float) phase);
    }

    private static float DoubleSoftWave(double phase) {
        return (0.8f * Mathf.Sin((float) phase)
                    + 0.4f * Mathf.Sin(0.5f * (float) phase)
                    + 0.2f * Mathf.Sin(0.25f * (float) phase)
                    ) / 1.2f;
    }

    private static float TripleSoftWave(double phase) {
        return (0.8f * Mathf.Sin((float) phase)
                    + 0.4f * Mathf.Sin(0.5f * (float) phase)
                    + 0.2f * Mathf.Sin(0.25f * (float) phase)
                    + 0.1f * Mathf.Sin(0.125f * (float) phase)
                    ) / 1.3f;
    }

    private static float SawtoothWave(double phase) {
        return (float) phase - Mathf.Floor((float)phase);
    }

    private static float ReverseSawtoothWave(double phase) {
        return 1 + Mathf.Floor((float)phase) - (float) phase;
    }

    private static float Attenuation(float time) {
        return Mathf.Min(1, Mathf.Exp(-4f * time));
    }

    private static float Attenuation2(float time) {
        return 16 * time * Mathf.Exp(-6 * time);
    }
}
