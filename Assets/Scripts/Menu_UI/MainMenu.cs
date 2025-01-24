using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class Buttons : MonoBehaviour
{
  //Load Scene

    private string[] score;

    public TMP_Text LeaderB1, LeaderB2, LeaderB3, LeaderB4, LeaderB5, LeaderB6, LeaderB7, LeaderB8, LeaderB9, LeaderB10;
    private string Score1, Score2, Score3, Score4, Score5, Score6, Score7, Score8, Score9, Score10; 

    public void Start()
    {
        score = new string[10];

        for(int i = 0 ; i<10;i++)
        {
            score[i] = Convert.ToString(PlayerPrefs.GetFloat(Convert.ToString(i)));

        }
        
        Score1 = "1st " + score[0];
        Score2 = "2nd " + score[1];
        Score3 = "3rd " + score[2];
        Score4 = "4th " + score[3];
        Score5 = "5th " + score[4];
        Score6 = "6th " + score[5];
        Score7 = "7th " + score[6];
        Score8 = "8th " + score[7];
        Score9 = "9th " + score[8];
        Score10 = "10th " + score[9];

        LeaderB1.text = Score1;
        LeaderB2.text = Score2;
        LeaderB3.text = Score3;
        LeaderB4.text = Score4;
        LeaderB5.text = Score5;
        LeaderB6.text = Score6;
        LeaderB7.text = Score7;
        LeaderB8.text = Score8;
        LeaderB9.text = Score9;
        LeaderB10.text = Score10;
        
        
    }

  public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void LeaderBoard()
    {
        SceneManager.LoadScene(2);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    //Quit Game
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player Said I quit");
    }

}