using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
keeps track of high scores and the mute button activeness
likely without needing cookies. 
*/
public class InformationKeeper
{
    static int runner_high_score = 0;
    static int score_attack_high_score = 0;
    static int memory_high_score = 0;
    static int time_attack_high_score = 0;
    static bool mute_active = false;

    public static int GetRunnerHighScore()
    {
        return runner_high_score;
    }

    public static void SetRunnerHighScore(int new_score)
    {
        runner_high_score = new_score;
    }

    public static int GetScoreAttackHighScore()
    {
        return score_attack_high_score;
    }

    public static void SetScoreAttackHighScore(int new_score)
    {
        score_attack_high_score = new_score;
    }

    public static int GetMemoryHighScore()
    {
        return memory_high_score;
    }

    public static void SetMemoryHighScore(int new_score)
    {
        memory_high_score = new_score;
    }

    public static int GetTimeAttackHighScore()
    {
        return time_attack_high_score;
    }

    public static void SetTimeAttackHighScore(int new_score)
    {
        time_attack_high_score = new_score;
    }
    
    public static void Mute()
    {
        mute_active = true;
    }

    public static void Unmute()
    {
        mute_active = false;
    }

    public static bool GetMuteActiveStatus()
    {
        return mute_active;
    }
}