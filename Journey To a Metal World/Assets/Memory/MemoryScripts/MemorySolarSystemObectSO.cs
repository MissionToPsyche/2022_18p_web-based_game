using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Solar System Object", fileName = "New Solar Object")]
public class MemorySolarSystemObectSO : ScriptableObject
{
    [SerializeField] string object_name = "Enter solar system object name here";
    [SerializeField] int number = 0;


    public string getName()
    {
        return object_name;
    }


    public int getNumber()
    {
        return number;
    }
}
