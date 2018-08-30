using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAndBlock : MonoBehaviour
{
    #region Variables
    public PlayerController player;
    public GameObject enemy;
    public int whichPlayer;

    [Header("Stab")]
    public GameObject stabCol;
    public float stabForce = 40;
    public float startStabTime;
    public float stabTimer;

    [Header("Block")]
    public GameObject blockCol;
    public bool isBlocking;
    public float blockSpeed = 2;
    public float blockTimer, maxBlockTimer = 5;

    [Header("Swipe")]
    public GameObject swipeCol;


    #endregion

    // Use this for initialization
    void Start()
    {
        player = GetComponent<PlayerController>();
        enemy = player.enemy;
        stabCol = GameObject.Find("StabCol" + whichPlayer);
        blockCol = GameObject.Find("BlockCol" + whichPlayer);
        swipeCol = GameObject.Find("SwipeCol" + whichPlayer);
        stabCol.active = false;
        blockCol.active = false;
        swipeCol.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        blockTimer -= Time.deltaTime;
        stabTimer -= Time.deltaTime;
        #region Stab
        if (Input.GetButtonDown("Stab" + whichPlayer))
        {
            stabTimer = startStabTime;
        }
        if (Input.GetButton("Stab" + whichPlayer) && stabTimer > 0 && !Input.GetButton("Block" + whichPlayer) && !Input.GetButton("Swipe" + whichPlayer))
        {
            stabCol.active = true;
            GetComponent<Rigidbody>().velocity = (new Vector3((transform.rotation.y/90) * stabForce, GetComponent<Rigidbody>().velocity.y,0));
        }
        else
        {
            stabCol.active = false;
            GetComponent<Rigidbody>().velocity = (new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0));
        }
        #endregion
        #region Block
        if (Input.GetButton("Block" + whichPlayer) && !Input.GetButton("Stab" + whichPlayer) && blockTimer < 0)
        {
            isBlocking = true;
        }
        else
        {
            isBlocking = false;
        }
        if (isBlocking)
        {
            Block();
        }
        else
        {
            blockCol.active = false;
            player.slowdownSpeed = 1;

        }
        #endregion
        #region Swipe
        if (Input.GetButtonDown("Swipe" + whichPlayer) && !Input.GetButton("Stab" + whichPlayer) && !Input.GetButton("Block" + whichPlayer))
        {
            swipeCol.active = true;
        }
        else
        {
            swipeCol.active = false;
        }
        #endregion
    }
    void Block()
    {
        blockCol.active = true;
        player.slowdownSpeed = blockSpeed;
    }
}
