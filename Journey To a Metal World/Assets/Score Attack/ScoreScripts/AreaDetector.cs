using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDetector : MonoBehaviour
{
    private SpriteRenderer research_area;
    private float shrink_speed;

    public GameObject ripple;
    private Animator ripple_animator;
    private SpriteRenderer ripple_sprite;

    void Start()
    {
        research_area = gameObject.GetComponent<SpriteRenderer>();
        shrink_speed = Random.Range(0.0001f, 0.000005f);
        ripple = GameObject.Find("Ripple Animation");
        ripple_animator = ripple.GetComponent<Animator>();
        ripple_sprite = ripple.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(StartScene.game_start == true){
            UpdatePosition();
            UpdateRippleOrder();
            
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

    void UpdateRippleOrder()
    {
        if(ripple_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1){
            ripple_sprite.sortingOrder = 0;
        }
        else{
            ripple_sprite.sortingOrder = 5;
        }
    }

    bool ResearchAreaExist()
    {
        return research_area;
    }

    /* Destroy the research area and update score when it is triggered by the ray */
    void OnTriggerEnter2D(Collider2D other) 
    {
        foreach (GameObject area in AreaGenerator.research_areas)
        {
            if(gameObject.transform.position == area.transform.position){
                if(ripple){
                    ripple_sprite.sortingOrder = 5;
                    ripple_animator.transform.position = area.transform.position;
                    ripple_animator.Play("Animation", 0, 0f);
                }
                AreaGenerator.research_areas.Remove(area);
                break;
            }
        }
        Destroy(gameObject);
        Destroy(other);
        Score.total_score += 10;
        AreaGenerator.cur_area_count--;
    }
}