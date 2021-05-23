using System;
using System.Collections.Generic;
using System.Text;

namespace SMO_Randomizer
{
    public static class StageUtil
    {
        static List<string> sLayerArr = new List<string>()
        {
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P"
        };

        public static int LayerToDictIndx(string layer)
        {
            return sLayerArr.IndexOf(layer);
        }
    }
}
