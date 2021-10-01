using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public static class ExtensionMonoBehavior
{
    public static void SetGameObjectActive(this MonoBehaviour target,bool isActive)
    {
        if (target == null) return;
        target.gameObject.SetActive(isActive);
    }
}
