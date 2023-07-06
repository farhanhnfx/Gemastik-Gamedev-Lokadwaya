using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizDataHelper : MonoBehaviour
{
    public static QuizDataHelper Instance {get; set;}
    [SerializeField]private TextAsset JSONFile;
    [SerializeField]private QuizSetting quizJSON;


    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }
    private void Start() {
        quizJSON = JsonUtility.FromJson<QuizSetting>(JSONFile.text);
    }
    public QuizSetting.QuizData GetQuizData(string levelId) {
        foreach (QuizSetting.QuizData data in quizJSON.setting) {
            if (data.level.Equals(levelId)) {
                return data;
            }
        }
        return null;
    }
}
