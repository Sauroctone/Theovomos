using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour {

    Spells activeSpell;
    public SpriteRenderer rend;
    public Sprite healSpr;
    public Sprite convertSpr;
    public Sprite iceSpr;
    public Sprite lightningSpr;
    public Sprite tornadoSpr;
    public Sprite emptySpr;
    public Controller player;

    public void AddSpell(Spells _spell)
    {
        activeSpell = _spell;

        switch (activeSpell)
        {
            case Spells.Convert:
                rend.sprite = convertSpr;
                break;
            case Spells.Heal:
                rend.sprite = healSpr;
                break;
            case Spells.IceWall:
                rend.sprite = iceSpr;
                break;
            case Spells.Lightning:
                rend.sprite = lightningSpr;
                break;
            case Spells.Tornado:
                rend.sprite = tornadoSpr;
                break;
        }
    }

    public void UseSpell()
    {
        switch (activeSpell)
        {
            case Spells.Convert:
                Convert();
                break;
            case Spells.Heal:
                Heal();
                break;
            case Spells.IceWall:
                player.state = PlayerStates.IceWall;
                break;
            case Spells.Lightning:
                player.state = PlayerStates.Lightning;
                break;
            case Spells.Tornado:
                Tornado();
                break;
        }
    }

    void Convert()
    {
        activeSpell = Spells.Null;
        rend.sprite = emptySpr;
    }

    void Heal()
    {

    }

    void Tornado()
    {

    }
}
