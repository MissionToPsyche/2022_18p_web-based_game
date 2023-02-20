using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartLocationsData
{
    public static Dictionary<string, Color> start_colors = new Dictionary<string, Color>() {
        {"Antenna Part Location", new Color(1f, 0.4f, 0.4f)},
        {"Magnetometer Part Location", new Color(1f, 1f, 0.6f)},
        {"Spectrometer Part Location", new Color(0.6f, 0.9f, 0.6f)},
        {"+Y Part Location", new Color(0.3f, 0.7f, 1f)},
        {"-Y Part Location", new Color(0.9f, 0.5f, 1f)}
    };

    public static Dictionary<string, Color> triggered_colors = new Dictionary<string, Color>() {
        {"Antenna Part Location", Color.red},
        {"Magnetometer Part Location", Color.yellow},
        {"Spectrometer Part Location", Color.green},
        {"+Y Part Location", Color.blue},
        {"-Y Part Location", Color.magenta}
    };
}
