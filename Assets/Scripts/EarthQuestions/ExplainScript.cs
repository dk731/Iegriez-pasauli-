using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplainScript : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartExplanation(string text)
    {


        GlobalVariables.FocusOn(Camera.main.transform, transform, 1.5f);

    }

}
