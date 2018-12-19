using UnityEngine;

namespace Services
{
    public class UserInputManager : IUserInputManager
    {
        public bool GetMouseButton(int button)
        {
            return Input.GetMouseButton(button);
        }

        public Vector3 GetMousePos()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}