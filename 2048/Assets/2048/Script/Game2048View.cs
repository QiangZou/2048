using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZQFramwork;

public class Game2048View : BaseView
{
    public int[] chess_0;
    public int[,] allChess;

    // Use this for initialization
    protected override void Start()
    {
        Debug.Log("123");
        //
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("您按下了W键");
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("您按下了S键");

        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("您按下了A键");
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("您按下了D键");
        }
    }



}
