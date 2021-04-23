using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueFalseQuestion
{
    public string text;
    public bool answer;
    public int id;

    private static int curQuestionCount = 0;
    public TrueFalseQuestion(int t)
    {
        text = "skajdhjasndl;asnd" + t;
        id = TrueFalseQuestion.curQuestionCount;

        TrueFalseQuestion.curQuestionCount++;
    }
}
