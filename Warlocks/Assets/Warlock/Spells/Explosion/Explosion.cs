using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public CircleCollider2D col;
    public PointEffector2D pointEffector;
    public int fixedUpdatesToExist = 1;

	void FixedUpdate()
    {
        if (fixedUpdatesToExist <= 1)
            Destroy(gameObject);
        else
            fixedUpdatesToExist--;
    }
}
