using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeGenerator : MonoBehaviour {

	float shakeTimer;
	float shakeAmount;
    Vector3 originPos;
    Coroutine shakeCor;

	void Start()
    {
        originPos = transform.position;
    }

	public void ShakeScreen (float shakeTmr, float shakeAmt)
	{
		shakeTimer = shakeTmr;
		shakeAmount = shakeAmt;
        shakeCor = StartCoroutine(ShakeCor());
	}

    IEnumerator ShakeCor()
    {
        while (shakeTimer > 0)
        {
            Vector2 shakeVector = Random.insideUnitCircle * shakeAmount;
            transform.position = new Vector3(originPos.x + shakeVector.x, originPos.y, originPos.z + shakeVector.y);
            shakeTimer -= Time.deltaTime;
            yield return null;
        }

        transform.position = originPos;
    }
}
