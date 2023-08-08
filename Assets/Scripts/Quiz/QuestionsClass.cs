using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionsClass
{
    public string question;
    public float answerFont;
    public List<AnswersClass> listOfAnswers = new List<AnswersClass>();
    [TextArea]
    public string explanation;


    private QuestionsClass()
    {

    }

    public QuestionsClass(string newQuestion)
    {
        this.question = newQuestion;
    }


    public QuestionsClass(string newQuestion, List<AnswersClass> newClass)
    {
        this.question = newQuestion;
        this.listOfAnswers = newClass;
    }

}
