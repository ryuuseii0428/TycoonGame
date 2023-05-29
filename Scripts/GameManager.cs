using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public int scanCharacterId;

    public Text talkText;
    public GameObject talkPanel;
    public bool isAction;
    public int talkIdex;

    public Vector3[] pointList;


    
    void Start()
    {
        GameLoad();
        Application.runInBackground = true;

    }

    void Update()
    {
        GameSave();
    }

    
    public void Action()
    {

        isAction = true;
        talkPanel.SetActive(true);
        talkPanel.SetActive(isAction);
        
    }

    public void GameSave()
    {
        PlayerPrefs.Save();

    }

    public void GameLoad()
    {
        if(!PlayerPrefs.HasKey("WalkLevel"))
        {
            return;
        }

        int sWalkLevel = PlayerPrefs.GetInt("WalkLevel");
        int sTotalWalk = PlayerPrefs.GetInt("TotalWalk");
        float sCurrentWalk = PlayerPrefs.GetFloat("CurrentWalk");
        float sMaxWalk = PlayerPrefs.GetFloat("MaxWalk");

    }

    public void GameExit()
    {
        GameSave();
        Application.Quit();
    }

    
    


   


}

