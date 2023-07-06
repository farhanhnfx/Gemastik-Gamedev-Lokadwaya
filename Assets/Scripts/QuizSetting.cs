using System;
using System.Collections.Generic;

[Serializable]
public class QuizSetting
{
    public List<QuizData> setting;
    
    [Serializable]
    public class QuizData
    {
        public string level;
        public string question;
        public string sideImgPath;
        public string sideAnswerKey;
        public List<QuizItem> items;
    }
    [Serializable]
    public class QuizItem
    {
        public int type;
        public string java;
    }
}
