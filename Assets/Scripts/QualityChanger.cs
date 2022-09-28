using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualityChanger : MonoBehaviour
{
    public void Low()
    {
        QualitySettings.SetQualityLevel(1);
    }
    public void VeryLow()
    {
        QualitySettings.SetQualityLevel(0);
    }
    public void Medium()
    {
        QualitySettings.SetQualityLevel(2);
        print(QualitySettings.GetQualityLevel());
    }
}
