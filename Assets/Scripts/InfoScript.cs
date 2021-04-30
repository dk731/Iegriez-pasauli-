using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        gameObject.GetComponent<OnClickScript>().clickFunctionList.Add(onClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void onClick()
    {
        Debug.Log("Info on click");
    }

    
}
