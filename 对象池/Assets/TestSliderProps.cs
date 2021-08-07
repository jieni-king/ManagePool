using UnityEngine;
using System.Collections;
using System;

public class TestSliderProps : MonoBehaviour
{
    public int value1 = 100;
    public int value2 = 2;
    public int readonlyValue
    {
        get { return value1 * value2; }
    }
}
