using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour {

    Spells activeSpell;

    [Header("Lightning")]
    public float minSwipeDist; 
    Vector3 endPos;
    Vector3 swipe;
    int currentTouch;
    RaycastHit hit;
    public LayerMask lightningLayer;
    public float lightningDelay;
    public float lightningLifetime;
    public float lightningInit;

    [Header("References")]
    public SpriteRenderer rend;
    public Sprite healSpr;
    public Sprite convertSpr;
    public Sprite iceSpr;
    public Sprite lightningSpr;
    public Sprite tornadoSpr;
    public Sprite emptySpr;
    public CombineManager altar;
    public GameObject lightningPart;
    public ScreenShakeGenerator shakeGen;
    public GameObject lightningBall;

    void Update()
    {
        switch (altar.state)
        {
            case PlayerStates.Normal:
                break;
            case PlayerStates.Lightning:
                if (activeSpell == Spells.Lightning)
                {
                    CheckLightning();
                }
                break;
        }
    }

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

    public void UseSpell(int _touch)
    {
        switch (activeSpell)
        {
            case Spells.Null:
                break;
            case Spells.Convert:
                Convert();
                break;
            case Spells.Heal:
                Heal();
                break;
            case Spells.IceWall:
                altar.state = PlayerStates.IceWall;
                break;
            case Spells.Lightning:
                altar.state = PlayerStates.Lightning;
                break;
            case Spells.Tornado:
                altar.state = PlayerStates.Tornado;
                break;
        }

        currentTouch = _touch;
    }

    void Convert()
    {
        activeSpell = Spells.Null;
        rend.sprite = emptySpr;
    }

    void Heal()
    {
        activeSpell = Spells.Null;
        rend.sprite = emptySpr;
    }

    void CheckLightning()
    {
        //print("check lightning");
        Touch touch = Input.GetTouch(currentTouch);
        //print(touch.phase);

        if (touch.phase == TouchPhase.Ended)
        {
            endPos = Camera.main.ScreenToWorldPoint(touch.position);
            swipe = endPos - transform.position;

            if (swipe.magnitude > minSwipeDist)
            {
                if (swipe.y > 0)
                {
                    StartCoroutine(LightningCor());
                }
            }

            altar.state = PlayerStates.Normal;
        }
    }

    IEnumerator LightningCor()
    {
        activeSpell = Spells.Null;
        rend.sprite = emptySpr;

        Vector3 originPos = transform.position;
        Vector3 direction = swipe;
        direction.z = 0f;
        direction = direction.normalized;
        bool touchedAltar = false;
        int impacts = 0;

        lightningBall.SetActive(true);

        yield return new WaitForSeconds(lightningInit);

        while (!touchedAltar && impacts < 3)
        {
            if (Physics.Raycast(originPos, direction, out hit, 15f, lightningLayer))
            {
                //Debug.DrawLine(originPos, direction * 15f, Color.red, 10f);                
                //print(hit.collider.name);

                GameObject lightning = Instantiate(lightningPart, originPos, Quaternion.LookRotation(Quaternion.Euler(0, 0, 90f) * direction)) as GameObject;
                Destroy(lightning, lightningLifetime);
                shakeGen.ShakeScreen(1f, .1f);

                impacts++;

                if (hit.collider.tag == "Altar_2")
                {
                    touchedAltar = true;
                    //feedback et perte de vie;
                }

                else
                {
                    originPos = hit.point;
                    direction = Quaternion.Euler(0, 0, 90f) * direction;
                    yield return new WaitForSeconds(lightningDelay);
                }
            }
        }

        lightningBall.SetActive(false);
    }
}
