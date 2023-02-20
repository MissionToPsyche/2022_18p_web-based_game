using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StartColors;

public class PartLocationsData
{
    public static Dictionary<string, Color> start_colors = new Dictionary<string, Color>() {
        {"Antenna Part Location", StartColors.START_RED},
        {"Magnetometer Part Location", StartColors.START_YELLOW},
        {"Spectrometer Part Location", StartColors.START_GREEN},
        {"+Y Part Location", StartColors.START_BLUE},
        {"-Y Part Location", StartColors.START_MAGENTA}
    };

    public static Dictionary<string, Color> triggered_colors = new Dictionary<string, Color>() {
        {"Antenna Part Location", Color.red},
        {"Magnetometer Part Location", Color.yellow},
        {"Spectrometer Part Location", Color.green},
        {"+Y Part Location", Color.blue},
        {"-Y Part Location", Color.magenta}
    };

    public static Dictionary<string, int> ids = new Dictionary<string, int>() {
        {"Antenna Part Location", 0},
        {"Magnetometer Part Location", 1},
        {"Spectrometer Part Location", 2},
        {"+Y Part Location", 3},
        {"-Y Part Location", 4}
    };
}
