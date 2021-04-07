using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZQFramwork;

public class Game2048ViewController : BaseView
{
    public Button btnNewGame;
  
    protected override void Start()
    {
        btnNewGame.onClick.AddListener(() =>
        {
            Debug.Log("重新开始游戏");
        });
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
