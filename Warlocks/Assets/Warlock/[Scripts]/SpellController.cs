using UnityEngine;

public class SpellController : MonoBehaviour
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
        var bullet = Instantiate(bulletPrefab, transform);
        bullet.GetComponent<Rigidbody2D>().AddForce((Vector2) transform.up * 6, ForceMode2D.Impulse);
        Destroy(bullet, 3.0f);
    }    
}
