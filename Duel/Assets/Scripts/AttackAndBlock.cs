﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAndBlock : MonoBehaviour
{
    #region Variables
    public PlayerController player;
    public int whichPlayer;
    [Header("Stab")]
    public GameObject stabCol;
    public float stabForce = 10;
    public float stabSlowdown;
    public float stabPower;
    public float stabDir;
    [Header("Block")]
    public GameObject blockCol;
    public float blockSpeed = 2;
    [Header("Swipe")]
    public GameObject swipeCol;


    #endregion

    // Use this for initialization
    void Start()
    {
        player = GetComponent<PlayerController>();
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
        #region Stab
        if (Input.GetButtonDown("Stab" + whichPlayer) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.Mouse0))
        {
            stabCol.active = true;
            stabPower = stabForce;
            stabDir = transform.rotation.y;
        }
        stabPower -= stabSlowdown;
        if (stabPower < 0)
        {
            stabPower = 0;
            stabCol.active = false;
        }
        #endregion
        #region Block
        if (Input.GetButton("Block" + whichPlayer) && stabPower <= 0)
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
        if (Input.GetButtonDown("Swipe" + whichPlayer) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.Mouse1))
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
