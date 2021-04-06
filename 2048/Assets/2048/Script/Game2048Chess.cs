using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game2048Chess
{
    /// <summary>
    /// 位置
    /// </summary>
    public Vector2Int position;
    /// <summary>
    /// 值
    /// </summary>
    public Game2048ChessType value;

    public Game2048Chess(Vector2Int position, Game2048ChessType value)
    {
        this.position = position;
        this.value = value;
    }
}
