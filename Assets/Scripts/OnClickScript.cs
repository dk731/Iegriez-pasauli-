using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickScript : MonoBehaviour
{
    
    public List<Action> functionList;

    void Start()
    {
        functionList = new List<Action>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Clicked()
    {
        foreach (Action func in functionList)
            func();
    }
}
