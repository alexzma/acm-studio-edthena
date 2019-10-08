using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackout : MonoBehaviour
{
    public Vector3 dir;
    public float speed;
    public float dur;
    public float initialDelay;
    public float maxPos;
    public Transform reset;

    public void Start()
    {
        StartCoroutine("BlackOut");
    }

    IEnumerator BlackOut()
    {
        yield return new WaitForSeconds(initialDelay);
        while (true)
        {
            while (transform.position.x > 0)
            {
                transform.position = transform.position + dir * speed * Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(dur);
            while (transform.position.x > maxPos)
            {
                transform.position = transform.position + dir * speed * Time.deltaTime;
                yield return null;
            }
            transform.position = reset.position;
            yield return new WaitForSeconds(dur);
        }
    }
}
