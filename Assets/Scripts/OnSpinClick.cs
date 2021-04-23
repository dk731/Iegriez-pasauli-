using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSpinClick : MonoBehaviour
{
    public Transform itemsHolder;

    private const float eul = 2.71828f;
    private float degressBetween = 72.0f;

    private float fullSumm;

    private int animationSteps;
    private int curAnimationStep = -1;

    private float curRotAndle = 0.0f;

    private float fullRotationAngle;

    public GameObject myWheel;

    private TrueFalseQuestions trueFalseScript;

    int randItemIndex;
    public TextAsset questionsFile;

    private List<string> questionObjectsList = new List<string>();
    private List<TrueFalseQuestion> tfQuestionList = new List<TrueFalseQuestion>();

    private float startAngle;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<OnClickScript>().functionList.Add(onClick);
        trueFalseScript = GameObject.Find("SecondScene").GetComponent<TrueFalseQuestions>();


        Dictionary<string, List<TrueFalseQuestion>> tmpDict = JsonConvert.DeserializeObject<Dictionary<string, List<TrueFalseQuestion>>>(questionsFile.text);

        List<string> tmpKeyList = new List<string>(tmpDict.Keys);
        for (int i = 0; i < 5; i++)
        {
            int tmpRand = Random.Range(0, tmpKeyList.Count);
            while (questionObjectsList.Contains(tmpKeyList[tmpRand]))
            {
                tmpRand = Random.Range(0, tmpKeyList.Count);
            }
            questionObjectsList.Add(tmpKeyList[tmpRand]);
        }

        randItemIndex = Random.Range(0, 5);

        List<TrueFalseQuestion> tmpCurList = tmpDict[questionObjectsList[randItemIndex]];
        HashSet<int> tmpIndexSet = new HashSet<int>();

        for (int i = 0; i < 5; i++)
        {
            int tmpRand = Random.Range(0, tmpCurList.Count);
            while (tmpIndexSet.Contains(tmpRand))
            {
                tmpRand = Random.Range(0, tmpKeyList.Count);
            }
            tfQuestionList.Add(tmpCurList[tmpRand]);
        }

        startAngle = Random.Range(0.0f, 360.0f);

        myWheel.transform.rotation = Quaternion.Euler(new Vector3(startAngle - 69.5f, 90, -90));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (curAnimationStep >=  0)
        {
            if (curAnimationStep < animationSteps)
            {
                curRotAndle += (myCustomFunc(curAnimationStep / (float)(animationSteps - 1)) / fullSumm) * fullRotationAngle;
                myWheel.transform.rotation = Quaternion.Euler(new Vector3(curRotAndle + startAngle - 69.5f, 90, -90));
                curAnimationStep++;
            }
            else
            {
                curAnimationStep = -1;
                StartCoroutine(NextStep());
            }
                
        }
    }

    IEnumerator NextStep()
    {
        yield return new WaitForSeconds(1);

        GameObject go = itemsHolder.GetChild(randItemIndex).GetChild(0).gameObject;

        trueFalseScript.StartQuestionary(go, tfQuestionList);
    }

    public float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    float myCustomFunc(float x)
    {
        float newX = Remap(x, 0.0f, 1.0f, 0.0f, 7.0f);

        // return (1.4f * ((newX + 0.2f) * (newX + 0.2f)) - 0.1f) / ((newX + 0.2f) * (newX + 0.2f) * (newX + 0.2f));
        return (8.1f * newX * Mathf.Pow(eul, -newX));
    }

    void onClick()
    {
        if (curAnimationStep == -1)
        {
            Debug.Log("Random item: " + randItemIndex);
            int fullRotations = Random.Range(4, 10);

            fullRotationAngle = randItemIndex * degressBetween + Random.Range(-0.4f, 0.4f) * degressBetween + fullRotations * 360 - startAngle;
            animationSteps = Mathf.RoundToInt(Random.Range(5.0f, 8.0f) / Time.fixedDeltaTime);

            fullSumm = 0.0f;

            for (int i = 0; i < animationSteps; i++)
                fullSumm += myCustomFunc(i / (float)(animationSteps - 1));

            curAnimationStep = 0;

            curRotAndle = 0;
        }
    }
}
