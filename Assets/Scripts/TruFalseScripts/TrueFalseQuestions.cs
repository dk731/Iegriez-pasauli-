using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrueFalseQuestions: MonoBehaviour
{
    // Start is called before the first frame update

    public float movementAnimationTime;
    public Transform newPos;
    public GameObject myQuestionObject;
    private QuestionScript myQuestionScript;

    public EarthScript earthScript;

    private GameObject myObjToMove;
    private float moveSpeed = 0;
    private float scaleSpeed;
    private float rotationSpeed;

    public int trueFalsePoints;
    private int questionCount;

    private int questionsAmount = 5;

    private float curAnimTime = -1.0f;
    private List<TrueFalseQuestion> tfQuestionList;

    void Start()
    {
        myQuestionScript = myQuestionObject.GetComponent<QuestionScript>();
    }   

    // Update is called once per frame
    void Update()
    {
        

    }

    IEnumerator MoveToNewPosAnim()
    {
        float curTime = 0;
        float stepSize = 0.01f;

        while (curTime <= movementAnimationTime)
        {
            float moveStep = moveSpeed * stepSize;
            float scaleStep = scaleSpeed * stepSize;
            float rotationStep = rotationSpeed * stepSize;
            curTime += stepSize;

            myObjToMove.transform.position = Vector3.MoveTowards(myObjToMove.transform.position, newPos.position, moveStep);
            myObjToMove.transform.localScale = Vector3.MoveTowards(myObjToMove.transform.localScale, newPos.localScale, scaleStep);
            myObjToMove.transform.rotation = Quaternion.RotateTowards(myObjToMove.transform.rotation, newPos.rotation, rotationStep);

            yield return new WaitForSeconds(stepSize);
        }

        myQuestionObject.SetActive(true);
        myQuestionScript.MakeInvisible();

        StartCoroutine(SpawnQuestion());
    }

    public void StartQuestionary(GameObject mainObject, List<TrueFalseQuestion> tfList)
    {

        tfQuestionList = tfList;
        myObjToMove = mainObject;
        myObjToMove.transform.parent = transform;

        moveSpeed = Vector3.Distance(myObjToMove.transform.position, newPos.position) / movementAnimationTime;
        scaleSpeed = Vector3.Distance(myObjToMove.transform.localScale, newPos.localScale) / movementAnimationTime;
        rotationSpeed = Quaternion.Angle(myObjToMove.transform.rotation, newPos.rotation) / movementAnimationTime;


        StartCoroutine(MoveToNewPosAnim());

        questionCount = 0;
    }
    
    public void questionCallback()
    {
        questionCount++;
        if (questionCount < questionsAmount)
        {
            StartCoroutine(SpawnQuestion());
        }
        else
        {
            myQuestionObject.SetActive(false);
            StartCoroutine(earthScript.StartEarth());
        }
            
    }
    IEnumerator SpawnQuestion()
    {

        yield return new WaitForSeconds(0.5f);

        myQuestionScript.SpawnQuestion(tfQuestionList[questionCount]);
    }
}
