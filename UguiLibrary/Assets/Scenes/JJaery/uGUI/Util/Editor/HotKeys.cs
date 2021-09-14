using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace JJaery.uGUI.Utils
{
    public class HotKeys : MonoBehaviour
    {

        [MenuItem("JJaery/uGUI/Hotkey/Image &#s")]
        static void CreateImage(MenuCommand menuCommand)
        {
            CreateUI<Image>(menuCommand);
        }

        [MenuItem("JJaery/uGUI/Hotkey/Text &#t")]
        static void CreateText(MenuCommand menuCommand)
        {
            CreateUI<Text>(menuCommand);
        }

        [MenuItem("JJaery/uGUI/Hotkey/RecyclingScrollView")]
        static void CreateRecyclingScrollView(MenuCommand menuCommand)
        {
            CreateUI(menuCommand, "RecyclingScrollView");
        }

        private static void CreateUI<T>(MenuCommand menuCommand) where T : Graphic
        {
            string typeName = typeof(T).Name;
            CreateUI(menuCommand, typeName);
        }

        private static void CreateUI(MenuCommand menuCommand, string overrideName = null)
        {
            GameObject selectedObject = menuCommand.context as GameObject;
            if (selectedObject == null) selectedObject = Selection.activeGameObject;

            if (selectedObject == null)
            {
                //신규 생성
                GameObject canvas = CreateUIGameObject<Canvas>(null);
                CreateUIGameObject(overrideName, canvas.transform);
                return;
            }
            else //선택한 친구가 있음
            {
                Canvas targetCanvas;

                if (selectedObject.transform.parent != null)
                {
                    targetCanvas = selectedObject.GetComponentInParent<Canvas>();
                }
                else
                {
                    targetCanvas = selectedObject.GetComponent<Canvas>();
                }

                if (targetCanvas == null)
                {
                    GameObject canvas = CreateUIGameObject<Canvas>(null);
                    Selection.activeGameObject = CreateUIGameObject(overrideName, canvas.transform);
                }
                else
                {
                    Selection.activeGameObject = CreateUIGameObject(overrideName, selectedObject.transform);
                }
            }
        }

        private static GameObject CreateUIGameObject<T>(Transform parent) where T : Behaviour
        {
            string typeName = typeof(T).Name;
            return CreateUIGameObject(typeName, parent);
        }


        private static GameObject CreateUIGameObject(string typeName, Transform parent)
        {
            string path = $"Template/{typeName}.prefab";
            GameObject obj = EditorGUIUtility.Load(path) as GameObject;
            if (obj != null)
            {
                var clone = Instantiate(obj, parent);
                clone.name = typeName;
                clone.transform.localPosition = Vector3.zero;
                return clone;
            }
            else
            {
                Debug.LogError($"Cannot find the Template Object - {path}");
                return null;
            }
        }
    }
}

