using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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

    public EarthScript earthScript;

    int randItemIndex;
    public TextAsset questionsFile;

    private List<string> questionObjectsList = new List<string>();
    private List<TrueFalseQuestion> tfQuestionList = new List<TrueFalseQuestion>();
    private MapQuestionHolder mapQuestions = new MapQuestionHolder();

    private float startAngle;

    // Start is called before the first frame update
    void Start()
    {
        var tmpDict = JsonConvert.DeserializeObject<List<ItemClass>>(questionsFile.text);

        int randObjInd = Random.Range(0, tmpDict.Count);
        ItemClass currentObj = tmpDict[randObjInd];

        randItemIndex = Random.Range(0, 5); // TMP SOLUTION!!!!

        foreach (int ind in GlobalVariables.genUniqueNumbers(0, tmpDict[randObjInd].tfQuestionsList.Count, 5))
            tfQuestionList.Add(currentObj.tfQuestionsList[ind]);

        FieldInfo[] fieldsList = currentObj.mapQuestions.GetType().GetFields();

        for (int i = 0; i < fieldsList.Length; i++)
        {
            string path = "Earth/" + fieldsList[i].Name.Substring(0, fieldsList[i].Name.Length - 13);
            GlobalVariables.iconSprites.Add(fieldsList[i].Name, Resources.Load<Sprite>(path));
            List<MapQuestion> currentList = (List<MapQuestion>)fieldsList[i].GetValue(currentObj.mapQuestions);
            List<MapQuestion> currentAddList = (List<MapQuestion>)mapQuestions.GetType().GetFields()[i].GetValue(mapQuestions);
            List<int> randIndexList = GlobalVariables.genUniqueNumbers(0, currentList.Count, 3);

            foreach (int j in randIndexList)
                currentAddList.Add(currentList[j]);
        }

        GlobalVariables.iconSprites.Add("right", Resources.Load<Sprite>("Earth/AnswerRightIcon"));
        GlobalVariables.iconSprites.Add("wrong", Resources.Load<Sprite>("Earth/AnswerWrongIcon"));

        earthScript.myQuestions = mapQuestions;

        startAngle = Random.Range(0, 20) * 18.0f + 9.0f;
        trueFalseScript = GameObject.Find("SecondScene").GetComponent<TrueFalseQuestions>();
        myWheel.transform.rotation = Quaternion.Euler(new Vector3(startAngle, 90, -90));
        gameObject.GetComponent<OnClickScript>().clickFunctionList.Add(onClick);
        GlobalVariables.gameScore = 0;
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
                myWheel.transform.rotation = Quaternion.Euler(new Vector3(curRotAndle + startAngle, 90, -90));
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
        yield return new WaitForSeconds(1.0f);

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
            int fullRotations = Random.Range(2, 5);
            int randSector = Random.Range(0, 4);

            float additionalAngle = -27.0f + randSector * 18.0f;

            fullRotationAngle = randItemIndex * degressBetween + fullRotations * 360 - startAngle + additionalAngle;
            animationSteps = Mathf.RoundToInt(Random.Range(6.0f, 9.0f) / Time.fixedDeltaTime);

            fullSumm = 0.0f;

            for (int i = 0; i < animationSteps; i++)
                fullSumm += myCustomFunc(i / (float)(animationSteps - 1));

            curAnimationStep = 0;

            curRotAndle = 0;
        }
    }
}
