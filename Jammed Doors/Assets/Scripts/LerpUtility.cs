using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpUtility {

    public static float EaseOut(float currentLerpTime, float lerpTime)
    {
        float t = currentLerpTime / lerpTime;
        t = Mathf.Sin(t * Mathf.PI * 0.5f);
        return t;
    }
    public static float EaseIn(float currentLerpTime, float lerpTime)
    {
        float t = currentLerpTime / lerpTime;
        t = 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
        return t;
    }
    public static float Exponential(float currentLerpTime, float lerpTime)
    {
        float t = currentLerpTime / lerpTime;
        t = t * t;
        return t;
    }
    public static float Smooth(float currentLerpTime, float lerpTime)
    {
        float t = currentLerpTime / lerpTime;
        t = t * t * (3f - 2f * t);
        return t;
    }
    public static float Smoother(float currentLerpTime, float lerpTime)
    {
        float t = currentLerpTime / lerpTime;
        t = t * t * (3f - 2f * t);
        return t;
    }
    public static Vector3 SineWave(float time, Vector3 pos, Vector3 dir, float amplitude = 1f, float period = 1f)
    {
        float theta = time / period;
        float distance = amplitude * Mathf.Sin(theta);
        return pos + dir * distance;
    }
}
