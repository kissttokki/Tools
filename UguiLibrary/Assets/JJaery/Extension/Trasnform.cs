using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class ExtensionTrasnform
{
    public static void DestroyChildrens(this Transform trans)
    {
        int count = trans.childCount;

        if (count == 0)
            return;

        for (int i = count - 1; i >= 0; --i)
        {
            GameObject.Destroy(trans.GetChild(i).gameObject);
        }
        trans.DetachChildren();
    }
}
