using System;
using UnityEngine;

namespace Utils
{
    public static class MouseTools
    {
        public static Vector3 GetMousePos()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
