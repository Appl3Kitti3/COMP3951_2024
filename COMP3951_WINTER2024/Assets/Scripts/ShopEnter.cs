using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopEnter : Gateway
{
    /*protected override GameObject[] GetMovableObjects()
    {
        GameObject[] tmp = base.GetMovableObjects();
        tmp[^1] = GameObject.Find("Enter First Room");
        return tmp;
    }*/

    protected override int GetNextScene()
    {
        return 2;
    }
}
