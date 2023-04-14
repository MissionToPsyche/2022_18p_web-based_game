using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDetector : MonoBehaviour
{
    private SpriteRenderer research_area;
    private float shrink_speed;

    void Start()
    {
        research_area = gameObject.GetComponent<SpriteRenderer>();
        shrink_speed = Random.Range(0.0001f, 0.000005f);
    }

    void Update()
    {
        if(StartScene.game_start == true){
            UpdatePosition();
        }
    }


    /* This function is to shrink the size of the research area for each update */
    void UpdatePosition()
    {
        if(ResearchAreaExist() && (research_area.transform.localScale.x >= 0.2f && research_area.transform.localScale.y >= 0.2f))
        {
            research_area.transform.localScale -= new Vector3(shrink_speed, shrink_speed, 0);
        }
    }

    bool ResearchAreaExist()
    {
        return research_area;
    }

    /* Destroy the research area and update score when it is triggered by the ray */
    void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log(gameObject.transform.position);
        Destroy(gameObject);
        Destroy(other);
        Score.total_score += 10;
        AreaGenerator.cur_area_count--;

        // if (GameEnds())
        // {
        //     Restart.restart = true;
        // }
    }

    // bool GameEnds()
    // {
    //     if(AreaGenerator.cur_area_count <= 0)
    //     {
    //         return true;
    //     }
    //     return false;
    // }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class AreaDetector : MonoBehaviour
// {
//     private SpriteRenderer research_area;
//     private float shrink_speed;
//     static int area_count = 0;

//     void Start()
//     {
//         research_area = gameObject.GetComponent<SpriteRenderer>();
//         shrink_speed = Random.Range(0.0001f, 0.000005f);
//         area_count += 1;
//     }

//     void Update()
//     {
//         UpdatePosition();
        
//     }

//     void UpdatePosition()
//     {
//         if(ResearchAreaExist() && (research_area.transform.localScale.x >= 0.2f && research_area.transform.localScale.y >= 0.2f))
//         {
//             research_area.transform.localScale -= new Vector3(shrink_speed, shrink_speed, 0);
//         }
//     }

//     bool ResearchAreaExist()
//     {
//         return research_area;
//     }

//     void OnTriggerEnter2D(Collider2D other) 
//     {
//         Destroy(gameObject);
//         Destroy(other);
//         Score.total_score += 10;
//         area_count--;

//         if (GameEnds())
//         {
//             Restart.restart = true;
//         }
//     }

//     bool GameEnds()
//     {
//         if(area_count <= 0)
//         {
//             return true;
//         }
//         return false;
//     }
// }


