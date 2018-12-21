using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shared
{
    public interface IUserInputManager
    {
        Vector3 GetMousePos();
        bool GetMouseButton(int button);
    }
}