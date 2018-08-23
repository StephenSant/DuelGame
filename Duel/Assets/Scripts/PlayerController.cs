using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [Header ("Movement")]
    public float moveSpeed = 2.5f;
    public float jumpHeight = 4;
    public float slowdownSpeed = 2;
    public bool grounded;
    public Rigidbody rb;
    Vector3 spawnPoint;
    public AttackAndBlock attackScript;
    #endregion

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        attackScript = GetComponent<AttackAndBlock>();
        spawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        #region GroundCheck
        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0, -1, 0)), 1.1f))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        #endregion
        #region Movement
        rb.velocity = (new Vector3((Input.GetAxisRaw("Horizontal") * (moveSpeed * 100) * Time.deltaTime + attackScript.stabPower) / slowdownSpeed, rb.velocity.y, 0));
        if (Input.GetButton("Jump") && grounded)
        {
            rb.velocity = (new Vector3(rb.velocity.x,(jumpHeight * 100) * Time.deltaTime, 0));
        }
        #endregion
        if (transform.position.y < -10) { transform.position = spawnPoint; }
    }
}
