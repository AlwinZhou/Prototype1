using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    Vector2 lastClickPos;
    bool moving = false;
   //[SerializeField] private GridManager GridSrcipt;
    [SerializeField] private GameObject controlPanel;
        Vector2 ClickPos;
    public TextMeshProUGUI currentRoundTEXT;
    public int currentRound=1;
    public int moveDistance = 4;

    //make an 2d array to store the cords of where the player can move to
    [SerializeField] private GameObject highlight2;
    //[SerializeField] private GameObject TileObj;
    //[SerializeField] private Tile TileScript;
    public GameObject[] highlight2Objects;

    [System.Serializable]
    public class Coordinate
    {
        public int x;
        public int y;

        public Coordinate(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }

    public Coordinate[,] coordinateArray;
    
    private void Start()
    {
        lastClickPos = transform.position ;

        highlight2Objects= GameObject.FindGameObjectsWithTag("Highlight2");


    }
    private void Update()
    {
        //initialze the 2d array
        int a, b;
        a = (int)transform.position.x - 3;
        b = (int)transform.position.y - 3;

        coordinateArray = new Coordinate[7, 7];
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                coordinateArray[i, j] = new Coordinate(a, b);
                a++;
                if (a == (int)transform.position.x + 4)
                {
                    a = (int)transform.position.x - 3;
                }
            }
            b++;
        }
        //set player movement
        if (Input.GetMouseButtonDown(0))
        {
              ClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //  Ray ray = camera.ScreenPointToRay(Input.mousePosition);
              float ClickDistance = Vector2.Distance(ClickPos, (Vector2)transform.position);
            if (ClickDistance < 1)
            {
                Debug.Log("player pos:" + transform.position);

                controlPanel.SetActive(true);
            }

            if (moving)
            {
                Debug.Log("player pos:" + transform.position);

                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)
                {

                    GameObject clickObj = hit.transform.gameObject;

                    if (clickObj.GetComponent<Tile>() != null&&Vector2.Distance(clickObj.transform.position,(Vector2)transform.position)< moveDistance)
                    {
                        lastClickPos = clickObj.transform.position;
                    }
                }

            }
        }
        if (moving&& (Vector2)transform.position != lastClickPos)
        {
            float step = speed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, lastClickPos, step);
            if ((Vector2)transform.position == lastClickPos)
            {
                moving = false;
                currentRound++;
                currentRoundTEXT.text = "Current Round:"+ currentRound.ToString();
                foreach (GameObject highlightObject in highlight2Objects) {
                    highlightObject.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }
    }

    public void OnMove()
    {
        moving = true;
        controlPanel.SetActive(false);
    }
    public void OnMove1()
    {
        moving = true;
        controlPanel.SetActive(false);
    }
    public void OnCancel()
    {
        controlPanel.SetActive(false);
    }

   /*  void OnTriggerStay(Collider other)
    {
        if (controlPanel.active == true) { highlight2.SetActive(true); }
    }
   */
}
