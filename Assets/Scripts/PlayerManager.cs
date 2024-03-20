using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //initailze the players
    public GameObject player1;
    public GameObject player2;
    public GameObject selectedPlayer;
    public GameObject[] PlayerArray;
    //player clicked
  


    //
    Vector2 ClickPos;
    //UI
    bool moving = false;
    [SerializeField]
    private GameObject controlPanel;
    //
    Vector2 lastClickPos;
   

    //
    [SerializeField]
    private GameObject Highligh2;
    [SerializeField]
    private GameObject[] HighlightArray;

    private void Start()
    {
        HighlightArray = GameObject.FindGameObjectsWithTag("Highlight2");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float ClickDistance1 = Vector2.Distance(ClickPos, (Vector2)player1.transform.position);
            float ClickDistance2 = Vector2.Distance(ClickPos, (Vector2)player2.transform.position);
            if (ClickDistance1 < 1)
            {
                selectedPlayer = player1;
                controlPanel.SetActive(true);
                selectedPlayer.GetComponent<PlayerMovement>().playerActive();

            }
            if (ClickDistance2 < 1)
            {
                selectedPlayer = player2;
                controlPanel.SetActive(true);
                selectedPlayer.GetComponent<PlayerMovement>().playerActive();

            }

            lastClickPos = selectedPlayer.transform.position;

            if (moving)
            {

                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)
                {
                    GameObject clickObj = hit.transform.gameObject;
                    if (clickObj.GetComponent<Tile>() != null && Vector2.Distance(clickObj.transform.position, (Vector2)selectedPlayer.transform.position) < 4)
                    {
                        lastClickPos = clickObj.transform.position;
                    }

                }

            }
          
        }
        if (moving && (Vector2)selectedPlayer.transform.position != lastClickPos)
        {
            float step = 10f * Time.deltaTime;
            Debug.Log("selectedPlayer.transform.position before move" + selectedPlayer.transform.position);
            Debug.Log("lastClickPosn" + lastClickPos);


            selectedPlayer.transform.position = Vector2.MoveTowards(selectedPlayer.transform.position, lastClickPos, step);
            Debug.Log("selectedPlayer.transform.position after move" + selectedPlayer.transform.position);

            if ((Vector2)selectedPlayer.transform.position == lastClickPos)
            {
                moving = false;
                selectedPlayer.GetComponent<PlayerMovement>().energy = false;
                selectedPlayer.GetComponent<PlayerMovement>().arrow.SetActive(false);
                foreach(GameObject tile in HighlightArray)
                {
                    tile.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }
    }

    public void OnMove()
    {
        moving = true;
        controlPanel.SetActive(false);
        selectedPlayer.GetComponent<PlayerMovement>().TileTrigger.SetActive(false);

    }
    public void OnCancel()
    {
        controlPanel.SetActive(false);
        selectedPlayer.GetComponent<PlayerMovement>().playerDiable();
        foreach (GameObject tile in HighlightArray)
        {
            tile.GetComponent<SpriteRenderer>().enabled = false;
        }

    }
    public void OnNext()
    {
        foreach(GameObject player in PlayerArray)
        {
            if(player.GetComponent<PlayerMovement>().energy == false)
            {
                return;
            }
        }
        int playerIndex = Array.IndexOf(PlayerArray, selectedPlayer);
        playerIndex++;
        if (playerIndex == PlayerArray.Length)
        {
            playerIndex = 0;
        }
            selectedPlayer = PlayerArray[playerIndex];
        while (selectedPlayer.GetComponent<PlayerMovement>().energy == false)
        {
            
            playerIndex++;
            if(playerIndex == PlayerArray.Length)
            {
                playerIndex = 0;
            }
            selectedPlayer = PlayerArray[playerIndex];
        }

        selectedPlayer.GetComponent<PlayerMovement>().playerActive();

    }

}
