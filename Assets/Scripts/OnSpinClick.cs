using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSpinClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<OnClickScript>().functionList.Add(onClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onClick()
    {
        Debug.Log("On wheel button click");
        // 1. Choose one random item
        // 2. Spin the wheel
    }
}
