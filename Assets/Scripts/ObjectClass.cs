using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClass
{
    public string name;
    public List<TrueFalseQuestion> tfQuestionsList;
    public MapQuestionHolder mapQuestions;
}

public class MapQuestionHolder
{
    public List<MapQuestion> transportQuestionsList;
    public List<MapQuestion> energyQuestionsList;
    public List<MapQuestion> productQuestionsList;
    public List<MapQuestion> tourismQuestionsList;
    public List<MapQuestion> wasteQuestionsList;

    public MapQuestionHolder ()
    {
        transportQuestionsList = new List<MapQuestion>();
        energyQuestionsList = new List<MapQuestion>();
        productQuestionsList = new List<MapQuestion>();
        tourismQuestionsList = new List<MapQuestion>();
        wasteQuestionsList = new List<MapQuestion>();
    }
}

public class MapQuestion
{
    public string questionText;
    public List<string> answerList;
    public string explanationText;
    public int answerID;
    public string locationName;
}

public class TrueFalseQuestion
{
    public string text;
    public bool answer;
    public int id;
}

