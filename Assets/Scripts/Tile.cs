using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor, offsetColor;
    //   [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject highlight;
    [SerializeField] private GameObject highlight2;
    [SerializeField] private PlayerMovement player;
    private Vector2 clickPos;
    private float ClickDistance;
    [SerializeField] private GameObject playerObj;
    [SerializeField] private GameObject controlPanel;

    public List<GameObject> listOfHighlight2Objects = new List<GameObject>();
    private void Start()
    {

        player = playerObj.GetComponent<PlayerMovement>();

        GameObject[] highlight2Objects = GameObject.FindGameObjectsWithTag("Highlight2");
        listOfHighlight2Objects.AddRange(highlight2Objects);

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
           
            clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ClickDistance = Vector2.Distance(clickPos, player.transform.position);

            if (controlPanel.active==true)
            {
                foreach (GameObject highlightObject in listOfHighlight2Objects)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            if (player.coordinateArray[i, j].x == highlightObject.transform.position.x &&
                                player.coordinateArray[i, j].y == highlightObject.transform.position.y)
                            {
                                highlightObject.SetActive(true);
                                Debug.Log("highlight2.transform.position: " + highlightObject.transform.position);
                            }
                        }
                    }
                }
            }
        }
       

    }
    private void OnMouseEnter()
    {
        if (controlPanel.active == true)
        {
            highlight.SetActive(false);
        }
        else
        {
            highlight.SetActive(true);

        }
    }
    private void OnMouseExit()
    {
        highlight.SetActive(false);
    }
}
