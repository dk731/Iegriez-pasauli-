using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLocationScript : MonoBehaviour
{
    public Sprite myIcon;

    private GameObject iconObject;
    private SpriteRenderer mySprite;
    private Animator iconAnimator;

    public bool active = true;

    private float lastHoverTime;
    private MapQuestion myQuestion;

    private MapQuestionManager questionManager;

    // Start is called before the first frame update
    void Start()
    {
        OnClickScript mouseScript = gameObject.GetComponent<OnClickScript>();
        mouseScript.hoverFunctionList.Add(OnHover);
        mouseScript.clickFunctionList.Add(OnClick);

        iconObject = transform.GetChild(0).gameObject;

        iconAnimator = iconObject.GetComponent<Animator>();
        mySprite = iconObject.GetComponent<SpriteRenderer>();
        // mySprite.sprite = myIcon;
        iconAnimator.SetFloat("RandomTime", Random.Range(0.8f, 1.2f));

        questionManager = GameObject.Find("QuestionHolder").GetComponent<MapQuestionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // iconObject.transform.LookAt(Camera.main.transform.position);
        Vector3 curRotation = transform.rotation.eulerAngles;
        curRotation.z = 0.0f;
        transform.rotation = Quaternion.Euler(curRotation);

    }

    public void SetupQuestion(MapQuestion question, Sprite icon)
    {
        myIcon = icon;
        mySprite.sprite = myIcon;
        myQuestion = question;
    }

    void OnClick()
    {
        if (myQuestion != null)
        {
            iconAnimator.SetFloat("RandomTime", 0.0f);
            iconAnimator.SetTrigger("Click");

            questionManager.SpawnQuestion(myQuestion, this);
        }
    }

    void OnHover()
    {
        if (myQuestion != null)
        {
            iconAnimator.SetBool("OnHover", true);
            lastHoverTime = Time.time;

            StartCoroutine(OnHoverDelay());
        }
    }

    IEnumerator OnHoverDelay()
    {
        yield return new WaitForSeconds(0.2f);

        if (Time.time - lastHoverTime > 0.2f)
            iconAnimator.SetBool("OnHover", false);
    }

    public void AnswerCallback(bool ansValue)
    {
        Debug.Log("Answer submited");
        if (ansValue)
            mySprite.sprite = GlobalVariables.iconSprites["right"];
        else
            mySprite.sprite = GlobalVariables.iconSprites["wrong"];
    }
}
