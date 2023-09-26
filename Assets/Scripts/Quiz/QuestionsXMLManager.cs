using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;   //Basic xml attributes
using System.Xml.Serialization; //access xmlserialiser
using System.IO; //file management

public class QuestionsXMLManager : MonoBehaviour
{
    public static QuestionsXMLManager instance { get; private set; }

    private void Awake()        //Singleton
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
          // LoadQuestion();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public QuestionDatabase questionDB;
    
    [System.Serializable]
    public class QuestionDatabase
    {
        [XmlArray("QuestionList")]
        public List<QuestionsClass> listOfQuestions = new List<QuestionsClass>();
    }


    //public void SaveQuestions()
    //{
    //    print("Questions saved");
    //    XmlSerializer serializer = new XmlSerializer(typeof(QuestionDatabase));
    //    FileStream stream = new FileStream(Application.dataPath + "/StreamingAssets/question_data.xml", FileMode.Create);
    //    serializer.Serialize(stream, questionDB);
    //    stream.Close();
    //}

    //public void LoadQuestion()
    //{
    //    print("Questions loaded");
    //    XmlSerializer serializer = new XmlSerializer(typeof(QuestionDatabase));
    //    //check the file exists before openning it
    //    FileStream stream = new FileStream(Application.dataPath + "/StreamingAssets/question_data.xml", FileMode.Open);
    //    questionDB = serializer.Deserialize(stream) as QuestionDatabase;
    //    stream.Close();

    //}
}