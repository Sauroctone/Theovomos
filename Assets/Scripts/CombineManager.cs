using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineManager : MonoBehaviour {

    public PlayerStates state;
    public int health;
    Elements firstElement;
    //public SlotManager slot1;
    //public SlotManager slot2;
    //public SlotManager slot3;
    //public SlotManager slot4;

    public SlotManager[] slots;
    //int fullSlots;

    void AddSpell(Spells _spell, Collider col)
    {
        firstElement = Elements.Null;

        bool storedSpell = false;
        foreach (SlotManager slot in slots)
        {
            if (slot.activeSpell == Spells.Null)
            {
                slot.AddSpell(_spell);
                storedSpell = true;
                break;
            }
        }

        if (!storedSpell)
        {
            for (int i = slots.Length-1; i > 0; i--)
            {
                slots[i].AddSpell(slots[i - 1].activeSpell);
            }
            slots[0].AddSpell(_spell);
        }

        Destroy(col.gameObject);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Fire")
        {
            switch (firstElement)
            {
                case Elements.Null:
                    firstElement = Elements.Fire;
                    Destroy(col.gameObject);
                    break;

                case Elements.Fire:
                    break;

                case Elements.Earth:
                    Erupt();
                    Destroy(col.gameObject);
                    break;

                case Elements.Air:
                    AddSpell(Spells.Lightning, col);
                    break;

                case Elements.Water:
                    AddSpell(Spells.Convert, col);
                    break;
            }
        }

        if (col.tag == "Water")
        {
            switch (firstElement)
            {
                case Elements.Null:
                    firstElement = Elements.Water;
                    Destroy(col.gameObject);
                    break;

                case Elements.Fire:
                    AddSpell(Spells.Convert, col);
                    break;

                case Elements.Earth:
                    AddSpell(Spells.Heal, col);
                    break;

                case Elements.Air:
                    AddSpell(Spells.IceWall, col);
                    break;

                case Elements.Water:
                    break;
            }
        }

        if (col.tag == "Air")
        {
            switch (firstElement)
            {
                case Elements.Null:
                    firstElement = Elements.Air;
                    Destroy(col.gameObject);
                    break;

                case Elements.Fire:
                    AddSpell(Spells.Lightning, col);
                    break;

                case Elements.Earth:
                    AddSpell(Spells.Tornado, col);
                    break;

                case Elements.Air:
                    break;

                case Elements.Water:
                    AddSpell(Spells.IceWall, col);
                    break;
            }
        }

        if (col.tag == "Earth")
        {
            switch (firstElement)
            {
                case Elements.Null:
                    firstElement = Elements.Earth;
                    Destroy(col.gameObject);
                    break;

                case Elements.Fire:
                    Erupt();
                    Destroy(col.gameObject);
                    break;

                case Elements.Earth:
                    break;

                case Elements.Air:
                    AddSpell(Spells.Tornado, col);
                    break;

                case Elements.Water:
                    AddSpell(Spells.Heal, col);
                    break;
            }
        }
    }

    public void UpdateHealth(int _healthMod)
    {
        health += _healthMod;

        if (health == 0)
        {
            //mourir
        }

        //feedback en fonction de la vie
    }

    void Erupt()
    {
        UpdateHealth(-1);
        firstElement = Elements.Null;
    }
}
