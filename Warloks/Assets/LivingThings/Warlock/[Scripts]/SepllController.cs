using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SepllController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Fire();
        }
    }

    void Fire()
    {
        var bullet = (GameObject) Instantiate(
            bulletPrefab,
            transform.position,
            transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce((Vector2) transform.up * 6, ForceMode2D.Impulse);
        Destroy(bullet, 3.0f);
    }    
}
