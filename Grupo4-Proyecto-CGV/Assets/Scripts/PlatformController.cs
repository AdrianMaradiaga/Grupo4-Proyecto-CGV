using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Rigidbody platformRB;
    public Transform[] platformPositions;
    public float platformSpeed;

    private int actualPosition = 0;
    private int nextPosition = 1;

    public bool moveToNext = true;
    public float waitTime;

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        if (moveToNext)
        {
            StopCoroutine(WaitForMove(0));
            platformRB.MovePosition(Vector3.MoveTowards(platformRB.position, platformPositions[nextPosition].position, platformSpeed * Time.deltaTime));
        }

        if (Vector3.Distance(platformRB.position, platformPositions[nextPosition].position) <= 0)
        {
            StartCoroutine(WaitForMove(waitTime));
            actualPosition = nextPosition;
            nextPosition++;

            if (nextPosition > platformPositions.Length - 1)
            {
                nextPosition = 0;
            }
        }
    }

    IEnumerator WaitForMove(float time)
    {
        moveToNext = false;
        yield return new WaitForSeconds(time);
        moveToNext = true;
    }
}
