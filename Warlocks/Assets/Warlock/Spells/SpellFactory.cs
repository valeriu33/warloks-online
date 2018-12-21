using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellFactory : MonoBehaviour
{
    public abstract float Range { get; }
    public abstract Spell Launch(Vector3 pos);
}
