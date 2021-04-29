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

    
}
