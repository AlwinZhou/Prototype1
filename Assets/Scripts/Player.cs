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






    // player propertites
    public GameObject TileTrigger;
    public GameObject arrow;
    public bool energy = true;



    private void Start()
    {


    }
    private void Update()
    {
    }

    public void playerActive()
    {
        TileTrigger.SetActive(true);
        arrow.SetActive(true);

    }
    public void playerDiable()
    {
        TileTrigger.SetActive(false);
        arrow.SetActive(false);

    }


}
