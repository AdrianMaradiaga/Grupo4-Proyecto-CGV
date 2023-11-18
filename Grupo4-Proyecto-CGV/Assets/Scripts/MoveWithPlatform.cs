using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithPlatform : MonoBehaviour
{
    CharacterController player;
    Vector3 groundPosition;
    Vector3 lastGroundPosition;
    string groundName;
    string lastGroundName;

    public float factorDivision = 4.2f;
    public Vector3 originOffSet;

    // Start is called before the first frame update
    void Start()
    {
        player = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.isGrounded)
        {
            RaycastHit hit;
            if (Physics.SphereCast(transform.position + originOffSet, player.radius / factorDivision, -transform.up, out hit))
            {
                GameObject groundedIn = hit.collider.gameObject;
                groundName = groundedIn.name;
                groundPosition = groundedIn.transform.position;

                if (groundPosition != lastGroundPosition && groundName == lastGroundName)
                {
                    this.transform.position += groundPosition - lastGroundPosition;
                    Vector3 platformVelocity = (groundPosition - lastGroundPosition) / Time.deltaTime;
                    player.Move(platformVelocity * Time.deltaTime);
                }

                lastGroundName = groundName;
                lastGroundPosition = groundPosition;
            }
        }
        //else if (!player.isGrounded)
        //{
        //    lastGroundName = null;
        //    lastGroundPosition = Vector3.zero;
        //}
    }

    private void OnDrawGizmos()
    {
        player = this.GetComponent<CharacterController>();
        Gizmos.DrawWireSphere(transform.position + originOffSet, player.radius / factorDivision); ;
    }
}
