using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineManager : MonoBehaviour {

    public PlayerStates state;
    Elements firstElement;
    public SlotManager slot1;
    public SlotManager slot2;
    public SlotManager slot3;
    public SlotManager slot4;
    int fullSlots;

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
                    //KABOOM
                    Destroy(col.gameObject);
                    break;

                case Elements.Air:
                    if (fullSlots == 0 || fullSlots == 4)
                        slot1.AddSpell(Spells.Lightning);
                    else if (fullSlots == 1)
                        slot2.AddSpell(Spells.Lightning);
                    else if (fullSlots == 2)
                        slot3.AddSpell(Spells.Lightning);
                    else if (fullSlots == 3)
                        slot4.AddSpell(Spells.Lightning);
                    fullSlots++;
                    if (fullSlots == 5)
                        fullSlots = 0;
                    Destroy(col.gameObject);
                    break;

                case Elements.Water:
                    if (fullSlots == 0 || fullSlots == 4)
                        slot1.AddSpell(Spells.Convert);
                    else if (fullSlots == 1)
                        slot2.AddSpell(Spells.Convert);
                    else if (fullSlots == 2)
                        slot3.AddSpell(Spells.Convert);
                    else if (fullSlots == 3)
                        slot4.AddSpell(Spells.Convert);
                    fullSlots++;
                    if (fullSlots == 5)
                        fullSlots = 0;
                    Destroy(col.gameObject);
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
                    if (fullSlots == 0 || fullSlots == 4)
                        slot1.AddSpell(Spells.Convert);
                    else if (fullSlots == 1)
                        slot2.AddSpell(Spells.Convert);
                    else if (fullSlots == 2)
                        slot3.AddSpell(Spells.Convert);
                    else if (fullSlots == 3)
                        slot4.AddSpell(Spells.Convert);
                    fullSlots++;
                    if (fullSlots == 5)
                        fullSlots = 0;
                    Destroy(col.gameObject);
                    break;

                case Elements.Earth:
                    if (fullSlots == 0 || fullSlots == 4)
                        slot1.AddSpell(Spells.Heal);
                    else if (fullSlots == 1)
                        slot2.AddSpell(Spells.Heal);
                    else if (fullSlots == 2)
                        slot3.AddSpell(Spells.Heal);
                    else if (fullSlots == 3)
                        slot4.AddSpell(Spells.Heal);
                    fullSlots++;
                    if (fullSlots == 5)
                        fullSlots = 0;
                    Destroy(col.gameObject);
                    break;

                case Elements.Air:
                    if (fullSlots == 0 || fullSlots == 4)
                        slot1.AddSpell(Spells.IceWall);
                    else if (fullSlots == 1)
                        slot2.AddSpell(Spells.IceWall);
                    else if (fullSlots == 2)
                        slot3.AddSpell(Spells.IceWall);
                    else if (fullSlots == 3)
                        slot4.AddSpell(Spells.IceWall);
                    fullSlots++;
                    if (fullSlots == 5)
                        fullSlots = 0;
                    Destroy(col.gameObject);
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
                    if (fullSlots == 0 || fullSlots == 4)
                        slot1.AddSpell(Spells.Lightning);
                    else if (fullSlots == 1)
                        slot2.AddSpell(Spells.Lightning);
                    else if (fullSlots == 2)
                        slot3.AddSpell(Spells.Lightning);
                    else if (fullSlots == 3)
                        slot4.AddSpell(Spells.Lightning);
                    fullSlots++;
                    if (fullSlots == 5)
                        fullSlots = 0;
                    Destroy(col.gameObject);
                    break;

                case Elements.Earth:
                    if (fullSlots == 0 || fullSlots == 4)
                        slot1.AddSpell(Spells.Tornado);
                    else if (fullSlots == 1)
                        slot2.AddSpell(Spells.Tornado);
                    else if (fullSlots == 2)
                        slot3.AddSpell(Spells.Tornado);
                    else if (fullSlots == 3)
                        slot4.AddSpell(Spells.Tornado);
                    fullSlots++;
                    if (fullSlots == 5)
                        fullSlots = 0;
                    Destroy(col.gameObject);
                    break;

                case Elements.Air:
                    break;

                case Elements.Water:
                    if (fullSlots == 0 || fullSlots == 4)
                        slot1.AddSpell(Spells.IceWall);
                    else if (fullSlots == 1)
                        slot2.AddSpell(Spells.IceWall);
                    else if (fullSlots == 2)
                        slot3.AddSpell(Spells.IceWall);
                    else if (fullSlots == 3)
                        slot4.AddSpell(Spells.IceWall);
                    fullSlots++;
                    if (fullSlots == 5)
                        fullSlots = 0;
                    Destroy(col.gameObject);
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
                    //KABOOM
                    Destroy(col.gameObject);
                    break;

                case Elements.Earth:
                    break;

                case Elements.Air:
                    if (fullSlots == 0 || fullSlots == 4)
                        slot1.AddSpell(Spells.Tornado);
                    else if (fullSlots == 1)
                        slot2.AddSpell(Spells.Tornado);
                    else if (fullSlots == 2)
                        slot3.AddSpell(Spells.Tornado);
                    else if (fullSlots == 3)
                        slot4.AddSpell(Spells.Tornado);
                    fullSlots++;
                    if (fullSlots == 5)
                        fullSlots = 0;
                    Destroy(col.gameObject);
                    break;

                case Elements.Water:
                    if (fullSlots == 0 || fullSlots == 4)
                        slot1.AddSpell(Spells.Heal);
                    else if (fullSlots == 1)
                        slot2.AddSpell(Spells.Heal);
                    else if (fullSlots == 2)
                        slot3.AddSpell(Spells.Heal);
                    else if (fullSlots == 3)
                        slot4.AddSpell(Spells.Heal);
                    fullSlots++;
                    if (fullSlots == 5)
                        fullSlots = 0;
                    Destroy(col.gameObject);
                    break;
            }
        }
    }
}
