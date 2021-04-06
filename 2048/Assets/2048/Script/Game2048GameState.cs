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
    /// 等待操作
    /// </summary>
    Awaits,
    /// <summary>
    /// 播放动画
    /// </summary>
    Play,
    /// <summary>
    /// 游戏结束
    /// </summary>
    Over,
}
