using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WallSpellFactory : SpellFactory
{
    public override float Range { get { return float.MaxValue; } }

    // DI
    private SpellController spellController;
    private DiContainer container;

    public GameObject wallPrefab;
    public float lifespan = 1;
    public float spawnOffset;

    [Inject]
    public void Construct(DiContainer container, SpellController spellController)
    {
        this.container = container;
        this.spellController = spellController;
    }

    public override Spell Launch(Vector3 pos)
    {
        var direction = (pos - spellController.spellSpawnPoint.position).normalized;
        var wallObj = Instantiate(wallPrefab, transform);

        wallObj.transform.position =
            spellController.spellSpawnPoint.position +
            direction * spawnOffset;
        LookAt2D(wallObj.transform, direction);

        Destroy(wallObj, lifespan);
        return wallObj.GetComponent<WallSpell>();
    }

    private void LookAt2D(Transform targetTransform, Vector3 direction)
    {
        var rot_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        targetTransform.rotation = Quaternion.Euler(0, 0, rot_z - 90);
    }
}
