using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
** Sets the borders at the edge of the screen.
*/

public class BorderManager : MonoBehaviour
{
    [SerializeField] Transform bot = null;
    [SerializeField] Transform top = null;
    [SerializeField] Transform left = null;
    [SerializeField] Transform right = null;

    private void Start()
    {
        Vector3 screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        float bodySize = bot.GetComponent<BoxCollider2D>().bounds.extents.y;// * bot.transform.localScale.y;

        bot.transform.position = new Vector2(0, -screenSize.y - bodySize);
        top.transform.position = new Vector2(0, +screenSize.y + bodySize);

        left.transform.position = new Vector2(-screenSize.x - bodySize, 0);
        right.transform.position = new Vector2(+screenSize.x + bodySize, 0);
    }
}
