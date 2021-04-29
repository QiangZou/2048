using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game2048ChessData
{
    public Game2048ChessType value;
    public bool isMerge;
    public Game2048ChessData()
    {
        value = Game2048ChessType.Number_2;
    }

    public Game2048ChessData(int value)
    {
        this.value = (Game2048ChessType)value;
    }

    public Game2048ChessData Clone()
    {
        Game2048ChessData data = new Game2048ChessData();
        data.value = value;
        data.isMerge = false;
        return data;
    }

    public Game2048Chessboard.MovieState GetMovieState(Game2048ChessData chessData)
    {
        if (chessData == null)
        {
            return Game2048Chessboard.MovieState.Movie;
        }
        if (chessData.isMerge)
        {
            return Game2048Chessboard.MovieState.Stop;
        }
        if (chessData.value == value)
        {
            return Game2048Chessboard.MovieState.Merge;
        }
        if (chessData.value != value)
        {
            return Game2048Chessboard.MovieState.Stop;
        }
        return Game2048Chessboard.MovieState.Empty;
    }

    public void Merge()
    {
        isMerge = true;
        value = (Game2048ChessType)((int)value * 2);
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        Game2048ChessData chessData = obj as Game2048ChessData;
        if (chessData == null)
        {
            return false;
        }

        return value == chessData.value;
    }
}
