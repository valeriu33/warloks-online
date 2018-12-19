using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderManager : MonoBehaviour
{

    [SerializeField] Transform bot = null;
    [SerializeField] Transform top = null;
    [SerializeField] Transform left = null;
    [SerializeField] Transform right = null;
    // Use this for initialization
    void Start ()
    {
        Vector3 screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        float bodySize = bot.GetComponent<BoxCollider2D>().bounds.extents.y;// * bot.transform.localScale.y;

        bot.transform.position = new Vector2(0, -screenSize.y - bodySize);
        top.transform.position = new Vector2(0, +screenSize.y + bodySize);

        left.transform.position = new Vector2(-screenSize.x - bodySize, 0);
        right.transform.position = new Vector2(+screenSize.x + bodySize, 0);
    }
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
