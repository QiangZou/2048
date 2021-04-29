using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZQFramwork;

public class Game2048Chess : BaseView
{
    public Text text;


    /// <summary>
    /// 值
    /// </summary>
    public Game2048ChessType value;
    public bool isMerge = false;

    public void Init(Game2048ChessType value)
    {
        this.value = value;

        text.text = ((int)value).ToString();


        iTween.ScaleFrom(gameObject, new Vector3(0.2f, 0.2f, 1f), 0.4f);
    }

    public void Movie(Game2048ChessType value)
    {
        this.value = value;
    }

    public void Movie(Transform position)
    {
        Movie(position.position);
    }

    public void Movie(Vector3 position)
    {
        iTween.MoveTo(gameObject, position, 0.4f);
        VPTimer.In(0.4f, () =>
        {
            text.text = ((int)value).ToString();
        });
    }

    public override string ToString()
    {
        return ((int)value).ToString();
    }
}
