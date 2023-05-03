using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AreaGenerator : MonoBehaviour
{
    public GameObject prefab;
    private int max_area_count = 8;
    public static int cur_area_count = 0;
    private float radius = 1.7f;
    private float research_area_radius = 0.5f;
    public static List<GameObject> research_areas = new List<GameObject>();

    void Start()
    {
        cur_area_count = 0;
        research_areas = new List<GameObject>();
    }

    void Update()
    {
        if(StartScene.game_start == true && cur_area_count < max_area_count)
        {
            var new_area_x = 0f;
            var new_area_y = 0f;
            var sign = 1;
            new_area_x = Random.Range(-1 * radius, radius);
            sign = GetRandomSign();
            new_area_y = Mathf.Sqrt(radius * radius - new_area_x * new_area_x) * sign;
            var redo = isOverlap(new_area_x, new_area_y);
            if(redo == false){
                var position = new Vector3(new_area_x, new_area_y, 0);
                GameObject new_research_area = Instantiate(this.prefab, position, transform.rotation) as GameObject;
                research_areas.Add(new_research_area);
                cur_area_count++;   
            }    
        }
    }

    int GetRandomSign()
    {
        int num = Random.Range(-1, 2);
        while(num == 0){
            num = Random.Range(-1, 1);
        }
        return num;
    }

    bool isOverlap(float new_area_x, float new_area_y){
        var distance = 0f;
        var exist_area_x = 0f;
        var exist_area_y = 0f;
        foreach (GameObject area in research_areas)
        {
            exist_area_x = area.transform.position.x;
            exist_area_y = area.transform.position.y;
            distance = Mathf.Sqrt(Mathf.Pow((exist_area_x - new_area_x), 2) + Mathf.Pow((exist_area_y - new_area_y), 2));
            if(distance < research_area_radius){
                return true;
            }
        }
        return false;
    }
}



