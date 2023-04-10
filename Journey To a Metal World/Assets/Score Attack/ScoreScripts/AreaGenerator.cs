using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AreaGenerator : MonoBehaviour
{
    public GameObject prefab;
    private int max_area_count = 8;
    public static int cur_area_count = 0;
    private float radius = 1.7f;
    // private GameObject[] research_areas;

    void Start()
    {
        cur_area_count = 0;
    }


    /* Generate a new research area if one gets detected and removed */
    void Update()
    {
        while(cur_area_count < max_area_count)
        {
            var x = Random.Range(-1 * radius, radius);
            var sign = GetRandomSign();
            var y = Mathf.Sqrt(radius * radius - x * x) * sign;
            var position = new Vector3(x, y, 0);
            GameObject new_research_area = Instantiate(this.prefab, position, transform.rotation) as GameObject;
            // research_areas.Add(new_research_area);
            cur_area_count++;
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
}
