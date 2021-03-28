using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game2048GameMode
{
    static List<Vector2> random = new List<Vector2>();
    /// <summary>
    /// 生成数字
    /// </summary>
    /// <param name="board"></param>
    /// <param name="location"></param>
    /// <returns></returns>
    public static bool SpawnNumber(int[,] board, ref Vector2 location)
    {
        random.Clear();

        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j] == 0)
                {
                    random.Add(new Vector2(i, j));
                }
            }
        }

        if (random.Count == 0)
        {
            return false;
        }

        int index = Random.Range(0, random.Count);
        location = random[index];
        return true;
    }


}
