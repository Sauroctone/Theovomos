using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour {

    public Spells activeSpell;
    bool concernedSlot;

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

    [Header("Ice Wall")]
    public float wallLifetime;

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
    public GameObject iceWall;

    void Update()
    {
        switch (altar.state)
        {
            case PlayerStates.Normal:
                break;
            case PlayerStates.Lightning:
                if (activeSpell == Spells.Lightning && concernedSlot)
                {
                    CheckLightning();
                }
                break;
            case PlayerStates.IceWall:
                if (activeSpell == Spells.IceWall && concernedSlot)
                {
                    CheckIceWall();
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
                concernedSlot = true;
                break;
            case Spells.Lightning:
                altar.state = PlayerStates.Lightning;
                concernedSlot = true;
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
        altar.UpdateHealth(+1);
        //feedback visuel;
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
            concernedSlot = false;
        }
    }

    void CheckIceWall()
    {
        Touch touch = Input.GetTouch(currentTouch);

        if (touch.phase == TouchPhase.Ended)
        {
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            touchPos.z = -2;
            GameObject wall = Instantiate(iceWall, touchPos, Quaternion.identity);
            Destroy(wall, wallLifetime);

            altar.state = PlayerStates.Normal;
            concernedSlot = false;
            activeSpell = Spells.Null;
            rend.sprite = emptySpr;
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
                Debug.DrawLine(originPos, direction * 15f, Color.red, 10f);                
                //print(hit.collider.name);

                GameObject lightning = Instantiate(lightningPart, originPos, Quaternion.LookRotation(Quaternion.Euler(0, 0, 90f) * direction)) as GameObject;
                Destroy(lightning, lightningLifetime);
                shakeGen.ShakeScreen(1f, .1f);

                impacts++;

                if (hit.collider.tag == "Altar_2")
                {
                    touchedAltar = true;
                    altar.UpdateHealth(-1);
                }


                else if (hit.collider.tag == "IceWall")
                {
                    impacts = 3;
                }

                else if (hit.collider.tag == "Wall")
                {
                    originPos = hit.point;
                    direction = Quaternion.Euler(0, 0, 90f) * direction;
                    yield return new WaitForSeconds(lightningDelay);
                }
            }

            else
                impacts++;
        }

        lightningBall.SetActive(false);
    }
}
