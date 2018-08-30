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
    public float stabSlowdown;
    public float stabPower;
    public float stabDir;
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
        blockTimer -= Time.deltaTime;
        #region Stab
        Vector3 towardsEnemy = (transform.position - enemy.transform.position).normalized;
        if (Input.GetButtonDown("Stab" + whichPlayer) && !Input.GetButton("Block" + whichPlayer) && !Input.GetButton("Swipe" + whichPlayer))
        {
            stabCol.active = true;
            stabPower = 10;
            stabDir = transform.rotation.y;
            GetComponent<Rigidbody>().AddForce(new Vector3(towardsEnemy.x,0,0) * -stabForce, ForceMode.Impulse);
        }
        stabPower -= stabSlowdown;
        if (stabPower < 0)
        {
            stabPower = 0;
            stabCol.active = false;
        }
        #endregion
        #region Block
        if (Input.GetButton("Block" + whichPlayer) && stabPower <= 0 && blockTimer < 0)
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
