using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartIndex
{
    public static Dictionary<string, int> PART_LOCATIONS = new Dictionary<string, int>() {
        {"Antenna Part Location", 0},
        {"Magnetometer Part Location", 1},
        {"Spectrometer Part Location", 2},
        {"+Y Part Location", 3},
        {"-Y Part Location", 4}
    };
}
