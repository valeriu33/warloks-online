using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services
{
    public interface IUserInputManager
    {
        Vector3 GetMousePos();
        bool GetMouseButton(int button);
    }
}