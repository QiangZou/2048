using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZQFramwork;

public class Game2048View : BaseMainView
{
    public Text textBaseScore;
    public Game2048Chessboard chessboard;


    protected override void Awake()
    {
        base.Awake();
        //Debug.Log(transform.name + " Awake222");
    }

    // Use this for initialization
    protected override void Start()
    {
        UpdateView();
    }


    private void UpdateView()
    {
        //设置最高分
        //设置当前分
        textBaseScore.text = (model.BaseViewData as Game2048ViewData).baseScore.ToString();


    }

    
}
