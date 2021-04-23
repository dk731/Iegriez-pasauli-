using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionScript : MonoBehaviour
{
    private TrueFalseQuestion myCurQuestion;
    public Material myMaterial;

    public TextMeshPro myText;
    public TextMeshPro noText;
    public TextMeshPro yesText;

    public float appearAnimTime;

    public TrueFalseQuestions myManager;

    private float curAnimTime = -1.0f;

    private bool fadeDir;
    public bool inAnimation;

    private bool receivedAnswer;


    void Start()
    {
        MakeInvisible();
    }


    // Update is called once per frame
    void Update()
    {
        if (inAnimation)
        {
            if (curAnimTime <= appearAnimTime)
            {
                float currentAlpha = fadeDir ? curAnimTime / appearAnimTime : 1 - curAnimTime / appearAnimTime;

                Color color = myMaterial.color;
                color.a = currentAlpha * 0.95f;
                myMaterial.color = color;
                color = myText.color;
                color.a = currentAlpha;
                myText.color = color;
                noText.color = color;
                yesText.color = color;

                curAnimTime += Time.deltaTime;
            }
            else 
            {
                inAnimation = false;
                if (!fadeDir)
                    MakeInvisible();
            }
        }

        


    }

    public void SpawnQuestion(TrueFalseQuestion question)
    {
        myCurQuestion = question;
        myText.text = myCurQuestion.text;
        
        fadeDir = true;
        curAnimTime = 0.0f;
        inAnimation = true;
    }

    public void MakeInvisible()
    {
        Color col = myMaterial.color;
        col.a = 0.0f;
        myMaterial.color = col;
        col = myText.color;
        col.a = 0.0f;
        myText.color = col;
        noText.color = col;
        yesText.color = col;
    }

    public void onAnswerSubmit(bool ans)
    {
        curAnimTime = 0;
        fadeDir = false;
        inAnimation = true;
        receivedAnswer = ans;

        StartCoroutine(QuestionCallbackDelayed());
    }

    IEnumerator QuestionCallbackDelayed()
    {
        yield return new WaitForSeconds(1.0f);

        myManager.questionCallback(myCurQuestion.answer == receivedAnswer);
    }
}
