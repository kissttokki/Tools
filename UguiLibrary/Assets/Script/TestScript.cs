using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public UIRecyclingScrollView sv;

    void Start()
    {
        sv.Initialize<TestRecyclingListItem>(100, (item) =>
        {
            Debug.Log("¤¾¤¾ ¶¸³×");
        });
    }
}
