using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class ExtensionRectTransform
{
    public static Bounds CalculateBounds(this RectTransform trans, float uiScaleFactor = 1)
    {
        Bounds bounds = new Bounds(trans.position, new Vector3(trans.rect.width, trans.rect.height, 0.0f) * uiScaleFactor);

        if (trans.childCount > 0)
        {
            foreach (RectTransform child in trans)
            {
                Bounds childBounds = new Bounds(child.position, new Vector3(child.rect.width, child.rect.height, 0.0f) * uiScaleFactor);
                bounds.Encapsulate(childBounds);
            }
        }

        return bounds;
    }

    private static Vector3[] GetCorners(this RectTransform trans)
    {
        Vector3[] corners = new Vector3[4];
        trans.GetWorldCorners(corners);
        return corners;
    }

    public static float GetMaxX(this RectTransform trans)
    {
        return trans.GetCorners()[2].x;
    }

    public static float GetMinX(this RectTransform trans)
    {
        return trans.GetCorners()[1].x;
    }

    public static float GetMinY(this RectTransform trans)
    {
        return trans.GetCorners()[0].y;
    }

    public static float GetMaxY(this RectTransform trans)
    {
        return trans.GetCorners()[1].y;
    }
}
