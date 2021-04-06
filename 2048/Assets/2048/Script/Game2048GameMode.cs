using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game2048GameMode
{
    static List<Vector2> random = new List<Vector2>();
    /// <summary>
    /// 生成位置
    /// </summary>
    /// <param name="board"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static bool SpawnLocation(int[,] board, out int x, out int y)
    {
        x = -1;
        y = -1;

        if (board == null)
        {
            return false;
        }

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
        x = (int)random[index].x;
        y = (int)random[index].y;
        return true;
    }

    public static bool Move(int[,] board)
    {
        if (board == null)
        {
            return false;
        }

        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                for (int k = j + 1; k < board.GetLength(1); k++)
                {
                    if (board[i, j] == 0 && board[i, k] != 0)
                    {
                        board[i, j] = board[i, k];
                        board[i, k] = 0;
                        break;
                    }
                    if (board[i, j] != 0 && board[i, j] == board[i, k])
                    {
                        board[i, j] += board[i, j];
                        board[i, k] = 0;
                        break;
                    }
                }
            }
        }

        return false;
    }
}
