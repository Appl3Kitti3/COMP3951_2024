using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetainCall
{
    public static bool AddAndCheckCounter(ref int counter)
    {
        counter++;
        return counter < 2;
    }
}
