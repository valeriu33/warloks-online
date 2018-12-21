using UnityEngine;

namespace Shared
{
    public class UserInputManager : IUserInputManager
    {
        public bool GetMouseButton(int button)
        {
            return Input.GetMouseButton(button);
        }

        public Vector3 GetMousePos()
        {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            return pos;
        }
    }
}