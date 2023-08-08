using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnswersClass
{
    public string answer;
    public bool isAnswer;

    private AnswersClass()
    {

    }

    public AnswersClass(string newAnswer, bool newBool)
    {
        this.answer = newAnswer;
        this.isAnswer = newBool;
    }
}
