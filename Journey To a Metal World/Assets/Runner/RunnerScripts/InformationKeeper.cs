using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
keeps track of high scores and the mute button activeness
likely without needing cookies. -- this is not necessarily tested yet but 
theoretically static variables shouldn't need to be stored in cookies I think

we didn't end up using it in the end product since we were given cookie
clearance but  I'm leaving it here as a back up just in case
*/
public class InformationKeeper
{
    static int runner_high_score = 0;
    static int score_attack_high_score = 0;
    static int memory_high_score = 0;
    static int time_attack_high_score = 0;
    static bool mute_active = false;

    /**
        returns the current high score for the runner game
        aka A Pebble in the Way
    */
    public static int GetRunnerHighScore()
    {
        return runner_high_score;
    }

    /**
        Sets a new high score for the runner game (A Pebble
        In the Way)
        Please note, this does not do any additional checks. it just sets it. 
    */
    public static void SetRunnerHighScore(int new_score)
    {
        runner_high_score = new_score;
    }

    /**
        returns the current high score for the Score Attack game 
        (Scan at Will)
    */
    public static int GetScoreAttackHighScore()
    {
        return score_attack_high_score;
    }

    /**
        Sets a new high score for the Score attack game 
        (Scan at will)
        Please not, this only sets it, there are no checks done in this function
    */
    public static void SetScoreAttackHighScore(int new_score)
    {
        score_attack_high_score = new_score;
    }

    /**
        returns the current high score for the Memory Game 
        (Among the Stars)
    */
    public static int GetMemoryHighScore()
    {
        return memory_high_score;
    }

    /**
        Sets a new high score for the Memory Game
        (among the stars)
        Note that no checks are made. this is a pure setter
    */
    public static void SetMemoryHighScore(int new_score)
    {
        memory_high_score = new_score;
    }

    /**
        returns the curernt high score for the Time Attack game 
        (Mechanical Madness)

    */
    public static int GetTimeAttackHighScore()
    {
        return time_attack_high_score;
    }

    /**
        Sets a new high score for the time attack game 
        (Mechanical Madness)
        remember no checks are made, this is just a setter
    */
    public static void SetTimeAttackHighScore(int new_score)
    {
        time_attack_high_score = new_score;
    }
    
    /**
        sets the mute_active static variable to be true
        representing that the game should have all sounds muted
    */
    public static void Mute()
    {
        mute_active = true;
    }

    /**
        sets the mute_active static vairable to be false
        representing that tha game should have sounds NOT muted
    */
    public static void Unmute()
    {
        mute_active = false;
    }

    /**
        returns the boolean status of the mute_active variable
    */
    public static bool GetMuteActiveStatus()
    {
        return mute_active;
    }
}