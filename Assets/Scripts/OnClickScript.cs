using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickScript : MonoBehaviour
{
    
    public List<Action> functionList = new List<Action>();
    public List<Action> hoverFuncs = new List<Action>();

    void Start()
    {
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

    public void OnHover()
    {
        foreach (Action func in hoverFuncs)
            func();
    }
}
