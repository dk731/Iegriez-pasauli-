using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MapQuestionManager : MonoBehaviour
{
    public List<GameObject> ansRocksList;

    public GameObject answerPref;

    public ExplainScript explanationScript;

    private MapQuestion currentQuestion;

    private GameObject answerHolder;

    private TextMeshPro questionText;

    private List<KeyValuePair<string, bool>> questionAnswers;

    private List<GameObject> currentAnswerList = new List<GameObject>();

    private MapLocationScript currentLocation;

    void Start()
    {
        questionText = transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>();
        answerHolder = transform.GetChild(1).gameObject;
    }

    void Update()
    {
        
    }

    public void SpawnQuestion(MapQuestion mp, MapLocationScript mls)
    {
        foreach (GameObject go in currentAnswerList)
            Destroy(go);
        currentAnswerList.Clear();
        currentLocation = mls;
        currentQuestion = mp;
        questionText.text = currentQuestion.questionText;

        List<int> randMeshes = GlobalVariables.genUniqueNumbers(0, currentQuestion.answerList.Count, currentQuestion.answerList.Count);

        questionAnswers = currentQuestion.answerList.ToList();

        float offsetStep = 5.0f / (questionAnswers.Count + 1);
        float curOffset = -3.0f + offsetStep;

        for (int i = 0; i < currentQuestion.answerList.Count; i++)
        {
            GameObject tmpAnsBtn = Instantiate(answerPref, answerHolder.transform);
            tmpAnsBtn.GetComponent<MapAnswerScript>().SetAnswer(questionAnswers[i], ansRocksList[randMeshes[i]], this);
            tmpAnsBtn.transform.localPosition = new Vector3(0.0f, 0.0f, curOffset);

            currentAnswerList.Add(tmpAnsBtn);
            curOffset += offsetStep;
        }


        StartCoroutine(GlobalVariables.FocusOn(Camera.main.transform, transform, 1.5f));
    }

    IEnumerator AnswerFocusCamera()
    {
        yield return new WaitForSeconds(1.0f);

        Vector3 targetDirection = (transform.position - Camera.main.transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        float animationStep = 0.01f;
        float animationTime = 1.5f;
        float currentAnimationTime = 0.0f;
        float rotationSpeed = Quaternion.Angle(transform.rotation, targetRotation) / animationTime * animationStep;

        while (currentAnimationTime <= animationTime)
        {
            Camera.main.transform.rotation = Quaternion.RotateTowards(Camera.main.transform.rotation, targetRotation, rotationSpeed);
            currentAnimationTime += animationStep;
            yield return new WaitForSeconds(animationStep);
        }
    }

    public void OnAsnwerSubmit(bool answerValue)
    {
        currentLocation.AnswerCallback(answerValue);
        explanationScript.StartExplanation(currentQuestion.explanationText);
    }
}
