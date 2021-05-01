using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExplainScript : MonoBehaviour
{
    public TextMeshPro myText;
    public GameObject planet;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartExplanation(string text)
    {

        myText.text = text;
        StartCoroutine(GlobalVariables.FocusOn(Camera.main.transform, transform, 1.5f));
        StartCoroutine(ReturnToPlanet());
    }

    IEnumerator ReturnToPlanet()
    {
        yield return new WaitForSeconds(3.0f);

        StartCoroutine(GlobalVariables.FocusOn(Camera.main.transform, planet.transform, 1.5f));
    }

}
