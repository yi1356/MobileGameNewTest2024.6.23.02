using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestA : MonoBehaviour
{
    public GameObject aaa;
    private static TestA instance;
    public static TestA Instance
    {
        get { return instance; }
    }
    public  GameObject GetAAA()
    {
        return aaa;
    }


    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
    }

}
