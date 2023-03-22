using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotacioneje : MonoBehaviour
{
    public float newYAngle;
    public float oldYAngle;

    public Transform target;

    public float smoothSpeed;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StopAllCoroutines();
            newYAngle = oldYAngle - 90f;
            StartCoroutine(Rotate(newYAngle));
            oldYAngle = newYAngle;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            StopAllCoroutines();
            newYAngle = oldYAngle + 90f;
            StartCoroutine(Rotate(newYAngle));
            oldYAngle = newYAngle;
        }
    }

    IEnumerator Rotate(float targetAngle)
    {
        while (transform.rotation.y != targetAngle)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, targetAngle, 0f), 12f * Time.deltaTime);
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        yield return null;
    }
}
