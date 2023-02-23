using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Solar System Object Array", fileName = "New Solar Array")]
public class MemorySolarSystemArraySO : ScriptableObject
{
    [SerializeField] MemorySolarSystemObectSO[] solar_system_array = new MemorySolarSystemObectSO[9];
    [SerializeField] int max_entries = 8;


    public MemorySolarSystemObectSO[] getArray()
    {
        return solar_system_array;
    }


    public MemorySolarSystemObectSO getSolarSystemObject(int index)
    {
        if (index < 0 || index > max_entries)
        {
            return null;
        }
        
        return solar_system_array[index];
    }
}
