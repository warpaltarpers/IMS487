﻿ using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {


    public Text winText;
    public GameObject player;

    private int numOfEnemies;
    private int playerHealth;
    public checkpointController respawnPoint;

    public static GameController instance;
    void Awake()
    {
        instance = this;
    }


    // Use this for initialization
    void Start () {
        numOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        playerHealth = player.GetComponent<PlayerController>().playerHealth;

        winText.text = "";
	}
	
	// Update is called once per frame
	void Update () {

        playerHealth = player.GetComponent<PlayerController>().playerHealth;
        if(playerHealth <= 0)        {
            Destroy(player);
            Debug.Log("输出3");
            if (respawnPoint != null)
            {
                player = respawnPoint.CreatePlayer();
                Debug.Log("输出4");
            };
        }

        if (numOfEnemies <= 0)
        {
            winText.text = "You Win";
        }
        /*
         * End the game if there are no enemies left
         */

    }

    /*
     * This is currently public... needs to be reworked.
     * 
     * Updates numOfEnemies whenever an enemy is destroyed
     * 
     */
    public void UpdateEnemyCount()    {
        numOfEnemies--;
    }


}
