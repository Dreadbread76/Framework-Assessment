using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadixLSDBase10 : MonoBehaviour
{

    int radix = 10;
    int exponent = 1;
    int[] passes;
    int[] output;
   

    void RadixLSD10(int[] value)
    {
        int minValue = value[0];
        int maxValue = value[0];
        int i = 0;
        //Find the max and min value in value[]
       

        for (i = 0; i < value.Length; i++)
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


        while((maxValue - minValue) / exponent >= 1)
        {
            value = CountValuesToSort(value, radix, exponent, minValue);

            exponent *= radix;
        }

        Debug.Log(value);

    }

    int[] CountValuesToSort(int[] value, int radix, int exponent, int minValue)
    {
        int newValue;
        int passIndex;
        passes = new int[radix];
        output = new int[value.Length];

        // Start the passes
        for ( newValue = 0; newValue < radix; newValue++)
        {
            passes[newValue] = 0;
        }

        for (newValue = 0; newValue < value.Length; newValue++)
        {
            passIndex = Mathf.FloorToInt(((value[newValue] - minValue) / exponent) % radix);
            passes[passIndex]++;
        }

        for (newValue = 1; newValue < radix; newValue++)
        {
            passes[newValue] += passes[newValue - 1];
        }
        //Sort the items in the array
        for (newValue = value.Length - 1; newValue >= 0; newValue--)
        {
            passIndex = Mathf.FloorToInt(((value[newValue] - minValue) / exponent) % radix);
            output[--passes[passIndex]] = value[newValue];
        }
        //set value to the new output
        for (newValue = 0; newValue < value.Length; newValue++)
        {
            value[newValue] = output[newValue];
        }
        return(value);
    }
   
}
