using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAndBlock : MonoBehaviour
{
    #region Variables
    public PlayerController player;
    [Header("Stab")]
    public GameObject stabCol;
    public float stabForce = 10;
    public float stabSlowdown;
    public float stabPower;
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
        stabCol = GameObject.Find("StabCol");
        blockCol = GameObject.Find("BlockCol");
        swipeCol = GameObject.Find("SwipeCol");
        stabCol.active = false;
        blockCol.active = false;
        swipeCol.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        #region Stab
        if (Input.GetKeyDown(KeyCode.Mouse1) && !Input.GetKey(KeyCode.S)&& !Input.GetKey(KeyCode.Mouse0))
        {
            stabCol.active = true;
            stabPower = stabForce;
        }
        stabPower -= stabSlowdown;
        if (stabPower < 0)
        {
            stabPower = 0;
            stabCol.active = false;
        }
        #endregion
        #region Block
        if (Input.GetKey(KeyCode.S)&&stabPower<=0)
        {
            blockCol.active = true;
            player.slowdownSpeed = blockSpeed;
        }
        else
        {
            blockCol.active = false;
            player.slowdownSpeed = 1;
        }
        #endregion
        #region Swipe
        if (Input.GetKeyDown(KeyCode.Mouse0) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.Mouse1))
        {
            swipeCol.active = true;
        }
        else
        {
            swipeCol.active = false;
        }
        #endregion
    }
}
