using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickScript : MonoBehaviour
{
    
    public List<Action> clickFunctionList = new List<Action>();
    public List<Action> hoverFunctionList = new List<Action>();

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Clicked()
    {
        foreach (Action func in clickFunctionList)
            func();
    }

    public void OnHover()
    {
        foreach (Action func in hoverFunctionList)
            func();
    }
}
