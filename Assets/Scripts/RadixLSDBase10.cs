using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadixLSDBase10 : MonoBehaviour
{

    int radix = 10;
   

    void RadixLSD10(int value)
    {
        int minValue = value[0];
        int maxValue = value[0];

        for (int i = 0; i < value.Length; i++)
        {
            if(value[i] < minValue)
            {
                minValue = value[];
            }
            else if (value[i] > maxValue)
            {
                maxValue = value[];
            }
        }
    }

   
}
