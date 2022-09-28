using System;
using UnityEngine;

public class HelpfulFuncs
{

    public static Vector3 Norm1(Vector3 theVec)
    {
        if (Vector3.zero == theVec)
        {
            return Vector2.zero;
        }
        Vector3 newVec = theVec;
        float a = Mathf.Atan2(newVec.y, newVec.x);
        newVec = new Vector3(Mathf.Cos(a), Mathf.Sin(a), 0);
        return newVec;
    }

    public static Vector3 Norm1Turnc(Vector3 theVec, int num)
    {
        Vector3 newVec = theVec;
        float a = Mathf.Atan2(newVec.y, newVec.x);
        newVec = new Vector3(Mathf.Cos(a), Mathf.Sin(a), 0);
        double ten = Mathf.Pow(10, num);
        newVec = new Vector3(
            (float)(Math.Truncate((double)newVec.x * ten) / ten),
            (float)(Math.Truncate((double)newVec.y * ten) / ten),
            (float)(Math.Truncate((double)newVec.z * ten) / ten)
        );
        return newVec;
    }
    public static bool CheckIfBetween(Vector2 theVec, float angHigh, float angLow)
    {
        float a = Mathf.Atan2(theVec.y, theVec.x) * Mathf.Rad2Deg;
        if (a >= angLow && a <= angHigh)
        {
            return true;
        }
        return false;
    }
}
