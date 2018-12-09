using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private float maxHp = 100;
    public float MaxHp { get { return maxHp; } }

    [SerializeField] private Image hpBar = null;
    [SerializeField] private UnityEvent onDie = null;

    public float Health { get; private set; }

    private void Start()
    {
        SetHealth(maxHp);
    }

    private void Update()
    {
        if (hpBar != null)
        {
            hpBar.transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }

    public float TakeDmg(float dmg)
    {
        var initialHp = Health;

        SetHealth(Health - dmg);
        if (Health <= 0)
            Die();

        return initialHp - Health;
    }

    public void SetHealth(float health)
    {
        Health = Mathf.Clamp(health, 0, maxHp);
        UpdateHpBar();
    }

    public void Die()
    {
        onDie.Invoke();
    }

    private void UpdateHpBar()
    {
        if (hpBar == null)
            return;

        hpBar.fillAmount = Health / maxHp;
    }
}
