using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FireballSpellFactory : SpellFactory
{
    // DI
    private SpellController spellController;
    private DiContainer container;

    public FireballSpell fireballPrefab;
    public float flyDistance = 10;
    public float velocity = 10;
    public float dmg;
    public float impactVelocity;

    public override float Range { get { return float.MaxValue; } }

    [Inject]
    public void Construct(DiContainer container, SpellController spellController)
    {
        this.container = container;
        this.spellController = spellController;
    }

    public override Spell Launch(Vector3 pos)
    {
        var direction = (pos - spellController.spellSpawnPoint.position).normalized;
        var spell = Instantiate(fireballPrefab, transform);

        container.Inject(spell);
        spell.Construct(dmg, impactVelocity);

        spell.transform.position = spellController.spellSpawnPoint.position;
        spell.GetComponent<Rigidbody2D>().velocity = direction * velocity;

        var lifespan = flyDistance / velocity;
        Destroy(spell.gameObject, lifespan);
        return spell;
    }
}
