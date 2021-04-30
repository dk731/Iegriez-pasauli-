using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionScript : MonoBehaviour
{
    private TrueFalseQuestion myCurQuestion;
    public Material myMaterial;
    public GameObject mainCloud;

    public TextMeshPro myText;
    public TextMeshPro noText;
    public TextMeshPro yesText;

    public float appearAnimTime;

    public TrueFalseQuestions myManager;
    public bool inAnim = false;

    private MeshRenderer meshRenderer;


    void Start()
    {
        MakeInvisible();
        meshRenderer = mainCloud.GetComponent<MeshRenderer>();
    }


    // Update is called once per frame
    void Update()
    {


    }

    

    public void SpawnQuestion(TrueFalseQuestion question)
    {
        myCurQuestion = question;
        myText.text = myCurQuestion.text;
        
        StartCoroutine(AppearAnim(true));
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

    public IEnumerator QuestionCallbackDelayed(bool ans)
    {
        bool ansRes = ans == myCurQuestion.answer;

        float curTime = 0;
        float stepSize = 0.01f;
        int speed = 1;
        float animLen = 0.1f;
        float startCol = meshRenderer.material.color.r;
        float dif = 1.0f - startCol;

        Color color = meshRenderer.material.color;

        while (curTime >= 0)
        {
            float val = curTime / animLen * dif;

            if (ansRes)
            {
                color.g = val + startCol;

                color.r = startCol - val;
                color.b = startCol - val;
            }
            else
            {
                color.r = val + startCol;

                color.g = startCol - val;
                color.b = startCol - val;
            }

            meshRenderer.material.color = color;

            curTime += stepSize * speed;

            if (curTime > animLen)
            {
                speed *= -1;
                curTime += stepSize * speed;
            }

            yield return new WaitForSeconds(stepSize);

        }

        meshRenderer.material = myMaterial;

        StartCoroutine(AppearAnim(false));

        yield return new WaitForSeconds(0.5f);

        myManager.questionCallback();
    }

    IEnumerator AppearAnim(bool dir)
    {
        inAnim = true;
        float curTime = 0;
        Color color = myMaterial.color;
        Color textColor = myText.color;
        float stepSize = 0.03f;

        while (curTime <= appearAnimTime)
        { 
            color.a = dir ? curTime / appearAnimTime : 1 - curTime / appearAnimTime;
            textColor.a = color.a;
            Debug.Log("Current a: " + color.a);

            myMaterial.color = color;
            myText.color = textColor;
            noText.color = textColor;
            yesText.color = textColor;

            curTime += stepSize;
            yield return new WaitForSeconds(stepSize);
        }

        color.a = dir ? 1.0f : 0.0f;
        textColor.a = color.a;

        myMaterial.color = color;
        myText.color = textColor;
        noText.color = textColor;
        yesText.color = textColor;
        inAnim = false;
    }

}
