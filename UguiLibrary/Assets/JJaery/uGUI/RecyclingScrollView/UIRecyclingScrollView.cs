using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class UIRecyclingScrollView : ScrollRect
{
    #region Event
    public event Action<GameObject> onShowItem;
    public event Action<GameObject> onHideItem;
    #endregion

    public GameObject listItemPrefab;

    private Dictionary<int, MonoBehaviour> _cachingDict = new Dictionary<int, MonoBehaviour>();
    private int _curIndex;
    private Bounds _caculatedBounds;

    private RectTransform[] _itemList = null;

    protected override void Awake()
    {
        base.Awake();
        onValueChanged.AddListener(OnScrolling);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        onValueChanged.RemoveListener(OnScrolling);
    }

    public void Initialize<T>(int count, Action<T> onShow = null, Action<T> onHide = null) where T : MonoBehaviour
    {
        _cachingDict.Clear();
        content.DestroyChildrens();
        _curIndex = 0;

        #region ui
//        if (onShow != null)
//        {
//            onShowItem += (obj) =>
//            {
//                int id = obj.GetInstanceID();
//                if (_cachingDict.ContainsKey(id) == true && _cachingDict[id] != null)
//                {
//                    onShow?.Invoke(_cachingDict[id] as T);
//                }
//                else
//                {
//                    var script = obj.GetComponent<T>();
//                    if (script != null)
//                    {
//                        onShow?.Invoke(script);
//                        _cachingDict[id] = script;
//                    }
//                    else
//                    {
//#if UNITY_EDITOR
//                        Debug.LogError($"[RecyclingScrollView] Cannot Find Script. (type : {typeof(T).Name}");
//#endif
//                    }
//                }
//            };
//        }

//        if (onHide != null)
//        {
//            onHideItem += (obj) =>
//            {
//                int id = obj.GetInstanceID();
//                if (_cachingDict.ContainsKey(id) == true && _cachingDict[id] != null)
//                {
//                    onHide?.Invoke(_cachingDict[id] as T);
//                }
//                else
//                {
//                    var script = obj.GetComponent<T>();
//                    if (script != null)
//                    {
//                        onHide?.Invoke(script);
//                        _cachingDict[id] = script;
//                    }
//                    else
//                    {
//#if UNITY_EDITOR
//                        Debug.LogError($"[RecyclingScrollView] Cannot Find Script. (type : {typeof(T).Name}");
//#endif
//                    }
//                }
//            };
//        }
        #endregion

        RectTransform target = listItemPrefab.transform as RectTransform;
        var baseScript = listItemPrefab.GetComponent<T>();
        if(baseScript == null)
        {
#if UNITY_EDITOR
            Debug.LogError($"[RecyclingScrollView] the taget is not included target script. {typeof(T).Name}");
#endif
            return;
        }
        if (target == null)
        {
#if UNITY_EDITOR
            Debug.LogError($"[RecyclingScrollView] the taget is not RectTransform");
#endif
            return;
        }

        _caculatedBounds = target.CalculateBounds();
        //가로 스크롤 여부
        float sizeDeltaX = this.horizontal == true ? _caculatedBounds.size.x * count : _caculatedBounds.size.x;
        //세로 스크롤 여부
        float sizeDeltaY = this.vertical == true ? _caculatedBounds.size.y * count : _caculatedBounds.size.y;

        content.sizeDelta = new Vector2(sizeDeltaX, sizeDeltaY);

        int showCount = Mathf.CeilToInt((transform as RectTransform).rect.height / _caculatedBounds.size.y);

        _itemList = new RectTransform[showCount];

        for (int i = 0; i < showCount; ++i) 
        {
            var obj = Instantiate(listItemPrefab, content.transform);
            onShow?.Invoke(obj.GetComponent<T>());
            _itemList[i] = obj.transform as RectTransform;
        }
    }

    private void OnScrolling(Vector2 delta)
    {
        if (delta.y > 0)
        {
            var rectTrans = transform as RectTransform;
            var headTrans = _itemList[0].transform as RectTransform;
            var topPoint = headTrans.GetMaxY();
            Debug.Log($"{rectTrans.rect.yMax} -  {topPoint} result :  {rectTrans.rect.yMax > topPoint}");
            if ((rectTrans.rect.yMax > topPoint) == false)
            {

            }
            //bounds.Contains(headerBounds.center + Vector3.up * headerBounds.size.y);
            //Debug.Log("아웃!");
        }
    }
}
