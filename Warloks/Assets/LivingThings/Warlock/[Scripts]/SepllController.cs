using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SepllController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab = null;
    [SerializeField] private float projectileSpeed = 6;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GetComponent<Animator>().SetTrigger("Fire");
            Fire();
        }
    }

    void Fire()
    {
        var bullet = (GameObject) Instantiate(
            bulletPrefab,
            transform.position,
            transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce((Vector2) transform.up * projectileSpeed, ForceMode2D.Impulse);
        Destroy(bullet, 3.0f);
    }    
}
