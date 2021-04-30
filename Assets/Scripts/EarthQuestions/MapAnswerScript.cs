using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapAnswerScript : MonoBehaviour
{
    private Animator myAnimator;
    private float lastHoverTime;

    public TextMeshPro myText;

    private KeyValuePair<string, bool> myAnswer;

    private MapQuestionManager myCallbackScript;
    void Start()
    {
        OnClickScript tmpClickScipt = gameObject.GetComponent<OnClickScript>();
        tmpClickScipt.clickFunctionList.Add(OnClick);
        tmpClickScipt.hoverFunctionList.Add(OnHover);
        myAnimator = gameObject.GetComponent<Animator>();

        myAnimator.SetFloat("RandSpeed", UnityEngine.Random.Range(0.9f, 1.1f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClick()
    {
        myAnimator.SetTrigger("OnClick");

        myCallbackScript.OnAsnwerSubmit(myAnswer.Value);
    }

    void OnHover()
    {
        myAnimator.SetBool("OnHover", true);
        lastHoverTime = Time.time;
        StartCoroutine(OnHoverDelay());
    }

    IEnumerator OnHoverDelay()
    {
        yield return new WaitForSeconds(0.1f);

        if (Time.time - lastHoverTime > 0.1f)
            myAnimator.SetBool("OnHover", false);
    }

    public void SetAnswer(KeyValuePair<string, bool> answer, GameObject rock, MapQuestionManager callbackManager)
    {
        myCallbackScript = callbackManager;
        myAnswer = answer;
        myText.text = answer.Key;

        Instantiate(rock, transform);

    }
}
