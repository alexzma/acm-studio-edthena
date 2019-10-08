
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Possibly lerp color of emission to when boss teleports

public class Boss : MonoBehaviour
{
    public float moveSpeed;
    //public float initialSpinDelay;
    //public float spinDelay;
    public float spinSpeed;
    public float lookDur;
    public int lookIters;
    public float booSpawnInterval;
    public int variation;

    public Transform rally1;
    public Transform rally2;
    public Transform rally3;
    public Transform laserSpawn;
    public GameObject minion;
    public Transform[] minionSpawn;
    public GameObject eyelid1;
    public GameObject eyelid2;
    public GameObject player;
    public LayerMask levelMask;
    
    Transform innerShield;
    Transform outerShield;
    float surviveTime;
    
    // Child 1 - Inner Shield
    // Child 2 - Outer Shield

    private void Start()
    {
        //emission = transform.GetChild(0).gameObject;
        innerShield = transform.GetChild(1);
        outerShield = transform.GetChild(2);
        surviveTime = Time.time + 360;
        
        StartCoroutine("Act");
    }

    // Kill it by time - N rounds
    private void Update()
    {
        if (Time.time > surviveTime)
        {
            Debug.Log("YOU WINNN");
        }
    }

    IEnumerator Act()
    {
        int internal_rotateSpeed = 3;
        int internal_betweenWait = 4;
        float internal_lookWait = lookDur * 2 + 4;
        //StartCoroutine(RotateRange(-25, 25, spinSpeed));
        StartCoroutine("SpawnBoos");
        while (true)
        {
            if (variation == 1)
            {
                for (int i = 0; i < lookIters; i++)
                {
                    StartCoroutine(Look(lookDur));
                    yield return new WaitForSeconds(internal_lookWait);
                }
                StartCoroutine(Move(moveSpeed, rally2));
                StartCoroutine(Rotate(transform, 315, Vector3.forward, internal_rotateSpeed));
                yield return new WaitForSeconds(internal_betweenWait);
                for (int i = 0; i < lookIters; i++)
                {
                    StartCoroutine(Look(lookDur));
                    yield return new WaitForSeconds(internal_lookWait);
                }
                StartCoroutine(Move(moveSpeed, rally3));
                StartCoroutine(Rotate(transform, 315, Vector3.forward, internal_rotateSpeed));
                yield return new WaitForSeconds(internal_betweenWait);
                for (int i = 0; i < lookIters; i++)
                {
                    StartCoroutine(Look(lookDur));
                    yield return new WaitForSeconds(internal_lookWait);
                }
                StartCoroutine(Move(moveSpeed, rally2));
                StartCoroutine(Rotate(transform, 45, Vector3.forward, internal_rotateSpeed));
                yield return new WaitForSeconds(internal_betweenWait);
                for (int i = 0; i < lookIters; i++)
                {
                    StartCoroutine(Look(lookDur));
                    yield return new WaitForSeconds(internal_lookWait);
                }
                StartCoroutine(Move(moveSpeed, rally1));
                StartCoroutine(Rotate(transform, 45, Vector3.forward, internal_rotateSpeed));
                yield return new WaitForSeconds(internal_betweenWait);
            } else if (variation == 2)
            {
                for (int i = 0; i < lookIters; i++)
                {
                    StartCoroutine(Look(lookDur));
                    yield return new WaitForSeconds(internal_lookWait);
                }
                StartCoroutine(Move(moveSpeed, rally2));
                StartCoroutine(Rotate(transform, 45, Vector3.forward, internal_rotateSpeed));
                yield return new WaitForSeconds(internal_betweenWait);
                for (int i = 0; i < lookIters; i++)
                {
                    StartCoroutine(Look(lookDur));
                    yield return new WaitForSeconds(internal_lookWait);
                }
                StartCoroutine(Move(moveSpeed, rally3));
                StartCoroutine(Rotate(transform, 45, Vector3.forward, internal_rotateSpeed));
                yield return new WaitForSeconds(internal_betweenWait);
                for (int i = 0; i < lookIters; i++)
                {
                    StartCoroutine(Look(lookDur));
                    yield return new WaitForSeconds(internal_lookWait);
                }
                StartCoroutine(Move(moveSpeed, rally2));
                StartCoroutine(Rotate(transform, 315, Vector3.forward, internal_rotateSpeed));
                yield return new WaitForSeconds(internal_betweenWait);
                for (int i = 0; i < lookIters; i++)
                {
                    StartCoroutine(Look(lookDur));
                    yield return new WaitForSeconds(internal_lookWait);
                }
                StartCoroutine(Move(moveSpeed, rally1));
                StartCoroutine(Rotate(transform, 315, Vector3.forward, internal_rotateSpeed));
                yield return new WaitForSeconds(internal_betweenWait);
            }
        }
    }

    IEnumerator SpawnBoos()
    {
        while (true)
        {
            Instantiate(minion, minionSpawn[Random.Range(0, minionSpawn.Length)]);      // Spawn boo after rally2
            yield return new WaitForSeconds(booSpawnInterval);
        }
    }

    IEnumerator Move(float speed, Transform target)
    {
        while ((transform.position - target.position).magnitude > 2)
        {
            transform.position = transform.position + (target.position - transform.position).normalized * speed * Time.deltaTime;
            yield return null;
        }
        yield return null;
    }

    IEnumerator RotateRange(float startAngle, float endAngle, float speed)
    {
        float t = 0;
        int rev = 1;
        Quaternion from = Quaternion.Euler(0, 0, startAngle);
        Quaternion to = Quaternion.Euler(0, 0, endAngle);
        while (true)
        {
            innerShield.transform.localRotation = Quaternion.Lerp(from, to, t);
            t += (Time.deltaTime * rev) / speed;
            if (t > 1 || t < 0) rev *= -1;
            yield return null;
        }
    }

    // For art, can make shadow of laser, then active collider once last stage
    void Laser()
    {
        //Vector2 toPlayer = (player.transform.position - transform.position).normalized;
        //Vector3 temp = DirFromAngle(innerShield.transform.rotation.eulerAngles.z, false);
        //Vector3 dir = new Vector3(temp.x, -temp.y, temp.z);
        Vector3 dir = (laserSpawn.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(laserSpawn.position, dir, 500, levelMask);
        Debug.DrawRay(laserSpawn.position, 500*dir, Color.red, 4);
        if (hit && hit.collider.tag == "CarDestruct")
        {
            Destroy(hit.collider.gameObject);
        }
    }

    IEnumerator Look(float dur)
    {
        StartCoroutine("OpenEye");
        yield return new WaitForSeconds(1);
        Laser();
        yield return new WaitForSeconds(dur);
        //yield return new WaitForSeconds(dur + Random.Range(0,2));
        StartCoroutine("CloseEye");
    }

    // Replace these into Look when done!
    IEnumerator OpenEye()
    {
        float dur = Time.time + 2;
        while (Time.time < dur)
        {
            eyelid1.transform.Translate(Vector2.left * 0.8f * Time.deltaTime);
            eyelid2.transform.Translate(Vector2.right * 0.8f * Time.deltaTime);
            yield return null;
        }
        yield return null;
    }

    IEnumerator CloseEye()
    {
        float dur = Time.time + 2;
        while (Time.time < dur)
        {
            eyelid1.transform.Translate(Vector2.right * 0.8f * Time.deltaTime);
            eyelid2.transform.Translate(Vector2.left * 0.8f * Time.deltaTime);
            yield return null;
        }
        yield return null;
    }

    IEnumerator Rotate(Transform obj, float angle, Vector3 axis, float inTime)
    {
        float rotationSpeed = angle / inTime;
        //yield return new WaitForSeconds(initialSpinDelay);
        Quaternion startRotation = obj.rotation;
        float deltaAngle = 0;

        // rotate until reaching angle
        while (Mathf.Abs(deltaAngle) < angle)
        {
            deltaAngle += rotationSpeed * Time.deltaTime;
            deltaAngle = Mathf.Min(Mathf.Abs(deltaAngle), angle);

            obj.rotation = startRotation * Quaternion.AngleAxis(deltaAngle, axis);
            yield return null;
        }
            
        //yield return new WaitForSeconds(spinDelay);
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.z;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
    }
}
