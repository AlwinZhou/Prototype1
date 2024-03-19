using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //initailze the players
    public GameObject player1;
    public GameObject player2;
    public GameObject selectedPlayer;
    public GameObject[] PlayerArray;
    public PlayerMovement loopPlayer;

    //
    Vector2 ClickPos;
    //UI
    bool moving = false;
    [SerializeField]
    private GameObject controlPanel;
    //
    Vector2 lastClickPos;


    private void Start()
    {
        
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

            }
            if (ClickDistance2 < 1)
            {
                selectedPlayer = player2;
                controlPanel.SetActive(true);
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
            if (moving && (Vector2)selectedPlayer.transform.position != lastClickPos )
            {
                float step = 10* Time.deltaTime;
                Debug.Log("selectedPlayer.transform.position" + selectedPlayer.transform.position);
                Debug.Log("lastClickPosn" + lastClickPos);

                selectedPlayer.transform.position = lastClickPos;
                if ((Vector2)selectedPlayer.transform.position == lastClickPos)
                {
                    Debug.Log("if enter moving statement:");

                    moving = false;
                    selectedPlayer.GetComponent<PlayerMovement>().energy = false;
                }
            }
        }
    }




    public void OnMove()
    {
        moving = true;
        controlPanel.SetActive(false);
    }
    public void OnCancel()
    {
        controlPanel.SetActive(false);
    }
    public void OnNext()
    {
        int i = 0;
        int count = 0;
        int size = PlayerArray.Length;
        bool finish = false;
        while (!finish)
        {
            loopPlayer = PlayerArray[i].GetComponent<PlayerMovement>();
            if (loopPlayer.energy == false)
            {
                count++;
            }
            else
            {
                selectedPlayer = PlayerArray[i];
            }
            if(count == size)
            {
                finish = true;
            }
            i++;
            if (i == size)
            {
                i = 0;
            }
        }
    }

}
