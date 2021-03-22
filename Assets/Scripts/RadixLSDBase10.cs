using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadixLSDBase10 : MonoBehaviour
{

    int radix = 10;
    int exponent = 1;
    int passIndex;
    int[] passes;
    int[] output;

    void RadixLSD10(int[] value)
    {
        int minValue = value[0];
        int maxValue = value[0];

        for (int i = 0; i < value.Length; i++)
        {
            if(value[i] < minValue)
            {
                minValue = value[i];
            }
            else if (value[i] > maxValue)
            {
                maxValue = value[i];
            }
        }

        while((minValue - maxValue) / exponent >= 1)
        {
            value = 
        }


        for (int i = 0; i < radix; i++)
        {
            passes[i] = 0;
        }

        for (int i = 0; i < value.Length; i++)
        {
            passIndex = Mathf.FloorToInt(((value[i] - minValue) / exponent) % radix);
            passes[passIndex]++;
        }

        for (int i = 0; i < radix; i++)
        {
            passes[i] += passes[i - 1];
        }

        for (int i = value.Length - 1; i >= 0; i--)
        {
            passIndex = Mathf.FloorToInt(((value[i] - minValue) / exponent) % radix);
            output[--passes[passIndex]] = value[i];
        }
    }

    void CountValuesToSort(int value)
    {

       
    }
   
}
