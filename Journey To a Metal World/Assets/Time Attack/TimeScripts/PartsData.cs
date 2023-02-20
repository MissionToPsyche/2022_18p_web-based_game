using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StartColors;

public class PartsData
{
    public static Dictionary<int, Color> start_colors = new Dictionary<int, Color>() {
        {0, StartColors.START_RED},
        {1, StartColors.START_YELLOW},
        {2, StartColors.START_GREEN},
        {3, StartColors.START_BLUE},
        {4, StartColors.START_MAGENTA}
    };
}
