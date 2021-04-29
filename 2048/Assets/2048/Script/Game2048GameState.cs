using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Game2048GameState
{
    /// <summary>
    /// 游戏开始
    /// </summary>
    Begin,
    /// <summary>
    /// 生成数字
    /// </summary>
    Build,
    /// <summary>
    /// 游戏结束
    /// </summary>
    IsGameOver,
    /// <summary>
    /// 移动
    /// </summary>
    Movie,
}
