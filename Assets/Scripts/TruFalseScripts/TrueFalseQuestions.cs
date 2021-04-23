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
        if (curAnimTime >= 0)
        {
            if (curAnimTime < movementAnimationTime)
            {
                float moveStep = moveSpeed * Time.deltaTime;
                float scaleStep = scaleSpeed * Time.deltaTime;
                float rotationStep = rotationSpeed * Time.deltaTime;
                curAnimTime += Time.deltaTime;

                myObjToMove.transform.position = Vector3.MoveTowards(myObjToMove.transform.position, newPos.position, moveStep);
                myObjToMove.transform.localScale = Vector3.MoveTowards(myObjToMove.transform.localScale, newPos.localScale, scaleStep);

                myObjToMove.transform.rotation = Quaternion.RotateTowards(myObjToMove.transform.rotation, newPos.rotation, rotationStep);
            }
            else
            {
                StartCoroutine(SpawnQuestion());
                curAnimTime = -1.0f;
            }

        }

    }

    public void StartQuestionary(GameObject mainObject, List<TrueFalseQuestion> tfList)
    {
        tfQuestionList = tfList;
        myObjToMove = mainObject;
        myObjToMove.transform.parent = transform;

        moveSpeed = Vector3.Distance(myObjToMove.transform.position, newPos.position) / movementAnimationTime;
        scaleSpeed = Vector3.Distance(myObjToMove.transform.localScale, newPos.localScale) / movementAnimationTime;

        rotationSpeed = Quaternion.Angle(myObjToMove.transform.rotation, newPos.rotation) / movementAnimationTime;

        curAnimTime = 0;

        myQuestionObject.SetActive(true);

        trueFalsePoints = 0;
        questionCount = 0;
    }
    
    public void questionCallback(bool ans)
    {
        trueFalsePoints += ans ? 1 : 0;
        questionCount++;

        if (questionCount < questionsAmount)
        {
            StartCoroutine(SpawnQuestion());
        }
        else
            myQuestionObject.SetActive(false);
    }
    IEnumerator SpawnQuestion()
    {
        while (myQuestionScript.inAnimation)
            yield return new WaitForSeconds(0.1f);

        yield return new WaitForSeconds(0.5f);

        myQuestionScript.SpawnQuestion(tfQuestionList[questionCount]);
    }
}
