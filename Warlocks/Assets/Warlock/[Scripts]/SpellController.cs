using System;
using System.Collections;
using System.Collections.Generic;
using Shared;
using UnityEngine;
using Zenject;

public class SpellController : MonoBehaviour
{
    // DI
    private IUserInputManager inputManager;
    public SpellFactory[] spellFactories;

    public Transform spellSpawnPoint;

    private int currentlySelectedSpellId = -1;


    [Inject]
    public void Construct(IUserInputManager inputManager)
    {
        this.inputManager = inputManager;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (currentlySelectedSpellId >= 0)
            {
                LaunchSpell(currentlySelectedSpellId);
                DeselectSpell();
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
            SelectSpellId(0);
        if (Input.GetKeyDown(KeyCode.F))
            SelectSpellId(1);

        if (Input.GetKeyDown(KeyCode.Escape))
            DeselectSpell();
    }

    private void SelectSpellId(int spellId)
    {
        currentlySelectedSpellId = spellId;
    }

    private void DeselectSpell()
    {
        currentlySelectedSpellId = -1;
    }

    private bool LaunchSpell(int spellId)
    {
        if (spellId < 0 || spellId >= spellFactories.Length)
            return false;

        if (spellFactories[spellId] == null)
            return false;

        var mousePos = inputManager.GetMousePos();
        if (Vector3.Distance(transform.position, mousePos) > spellFactories[spellId].Range)
            return false;

        spellFactories[spellId].Launch(mousePos);
        return true;
    }
}
