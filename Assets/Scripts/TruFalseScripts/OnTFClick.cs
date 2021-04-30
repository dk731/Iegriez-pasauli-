using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTFClick: MonoBehaviour
{
    public QuestionScript myQuestionScript;
    public bool myValue;

    private bool hoverTimeout = false;
    private float lastHoverTime = -1;
    private Animator myAnimator;

    void Start()
    {
        OnClickScript tmpScript = gameObject.GetComponent<OnClickScript>();
        tmpScript.clickFunctionList.Add(OnClick);
        tmpScript.hoverFunctionList.Add(OnHover);
        myAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hoverTimeout)
        {
            myAnimator.SetBool("Hover", false);
        }
    }

    void OnHover()
    {
        myAnimator.SetBool("Hover", true);
        hoverTimeout = false;
        lastHoverTime = Time.time;
        StartCoroutine(HoverTimeOut());
    }

    IEnumerator HoverTimeOut()
    {
        yield return new WaitForSeconds(0.5f);
        hoverTimeout = lastHoverTime - Time.time < 0.5f;
    }

    void OnClick()
    {
        myAnimator.SetTrigger("On Click");
        StartCoroutine(myQuestionScript.QuestionCallbackDelayed(myValue));
    }


}
