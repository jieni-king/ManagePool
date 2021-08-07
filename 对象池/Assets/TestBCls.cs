using UnityEngine;
using System.Collections;
using System;

public class TestBCls : MonoBehaviour
{
    public int age;
    public string name;
    public int tall;
    public int power
    {
        get { return age * tall; }
    }

    public override string ToString()
    {
        return string.Format("TestBCls : age:{0}, name:{1}, tall:{2}, power:{3}", age, name, tall, power);
    }
}
