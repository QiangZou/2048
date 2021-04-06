using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game2048Chessboard
{
    public List<List<Game2048Chess>> chessboard;

    public Game2048Chessboard(int[,] allChess)
    {
        if (allChess == null) return;

        chessboard = new List<List<Game2048Chess>>();

        for (int i = 0; i < allChess.GetLength(0); i++)
        {
            List<Game2048Chess> chessList = new List<Game2048Chess>();

            for (int j = 0; j < allChess.GetLength(1); j++)
            {
                if (allChess[i, j] == 0)
                {
                    chessList.Add(null);
                }
                else
                {
                    Game2048Chess chess = new Game2048Chess(new Vector2Int(i, j), (Game2048ChessType)allChess[i, j]);
                    chessList.Add(chess);
                }
            }

            chessboard.Add(chessList);
        }
    }
}
