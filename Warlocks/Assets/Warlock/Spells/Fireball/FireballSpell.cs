using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FireballSpell : Spell
{
    // DI
    private SpellController spellController;

    public GameObject explosionPrefab;

    private float dmg;
    private float impactVel;

    [Inject]
    public void ZenjectConstruct(SpellController spellController)
    {
        this.spellController = spellController;
    }

    public void Construct(float dmg, float impactVelocity)
    {
        this.dmg = dmg;
        this.impactVel = impactVelocity;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        var other = col.collider;
        if (other.gameObject == spellController.gameObject)
            return;

        var otherHealthManager = other.GetComponent<HealthManager>();
        if (otherHealthManager == null)
            return;

        otherHealthManager.TakeDmg(dmg);

        var explosion = Instantiate(explosionPrefab, transform.parent).GetComponent<Explosion>();
        explosion.transform.position = transform.position;
        explosion.col.radius = GetComponent<CircleCollider2D>().radius * 1.1f;
        explosion.pointEffector.forceMagnitude = impactVel;

        Destroy(gameObject);
    }
}
