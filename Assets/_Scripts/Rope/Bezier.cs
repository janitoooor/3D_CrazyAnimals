﻿using UnityEngine;

public static class Bezier
{
    //F(t) = (1 - t)^3 * PO + 3 * (1 - t)^2 * t * P1 + 3 * (1 - t) * t^2 * P2 + t^3 * P3 
    public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;

        return
            Mathf.Pow(oneMinusT, 3) * p0 +
            3f * Mathf.Pow(oneMinusT, 2) * t * p1 +
            3f * oneMinusT * Mathf.Pow(t, 2) * p2 +
            Mathf.Pow(t, 3) * p3;
    }

    //F'(t) = 3 * ((1 - t)^2 * (P1 - P0) + 2 *(1 - t) * t * (P2 - P1) + t^2 * (P3 - P2))
    public static Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return 3 * (
            Mathf.Pow(oneMinusT, 2) * (p1 - p0) +
            2 * oneMinusT * t * (p2 - p1) +
            Mathf.Pow(t, 2) * (p3 - p2)
            );
    }

    public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return
            oneMinusT * oneMinusT * p0 +
            2f * oneMinusT * t * p1 +
            t * t * p2;
    }

    public static Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        return 2f * ((1f - t) * (p1 - p0) + t * (p2 - p1));
    }
}