using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [Header("Movement")]
    public float moveSpeed = 2.5f;
    public float jumpHeight = 4;
    public float slowdownSpeed = 2;
    public float knockbackForce = 10;
    public bool grounded;
    public Rigidbody rb;
    Vector3 spawnPoint;
    public AttackAndBlock attackScript;
    public GameObject enemy;

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
        rb.velocity = (new Vector3((Input.GetAxisRaw("Horizontal" + attackScript.whichPlayer) * (moveSpeed * 100)) * Time.deltaTime / slowdownSpeed, rb.velocity.y, 0));
        if (Input.GetButton("Jump" + attackScript.whichPlayer) && grounded)
        {
            rb.velocity = (new Vector3(rb.velocity.x, (jumpHeight * 100) * Time.deltaTime, 0));
        }
        #endregion
        if (transform.position.y < -10) { transform.position = spawnPoint; }
        transform.LookAt(new Vector3(enemy.transform.position.x, transform.position.y, 0));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "StabCol" && other.tag == enemy.transform.tag && !attackScript.isBlocking)
        {
            Debug.Log("Player "+attackScript.whichPlayer+": has Died!");
            transform.position = new Vector3(100, 10,0);
        }
        if (other.name == "StabCol" && other.tag == enemy.transform.tag && attackScript.isBlocking)
        {
            Vector3 knockBackDirection =  (transform.position  - enemy.transform.position).normalized;
            enemy.GetComponent<Rigidbody>().AddForce(new Vector3(knockBackDirection.x,0,0) * -knockbackForce, ForceMode.Impulse);
        }
        if (other.name == "SwipeCol" && other.tag == enemy.transform.tag)
        {
            attackScript.isBlocking = false;
            attackScript.blockTimer = attackScript.maxBlockTimer;
        }
        if (other.tag == "Enviroment")
        {
            transform.position = new Vector3(100, 10, 0);
        }
    }
}
