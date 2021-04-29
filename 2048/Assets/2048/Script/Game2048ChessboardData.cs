using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game2048ChessboardData
{
    private const int Chessboardlength = 4;
    private const int BuildNumber = 2;

    private Game2048GameState gameState;
    private Dictionary<Vector2Int, Game2048ChessData> chessboardDic;
    public Game2048ChessboardData()
    {
        SwitchState(Game2048GameState.Begin);
    }

    public Game2048ChessboardData(int[,] chessboard, Game2048GameState gameState)
    {
        chessboardDic = new Dictionary<Vector2Int, Game2048ChessData>();
        for (int i = 0; i < chessboard.GetLength(0); i++)
        {
            for (int j = 0; j < chessboard.GetLength(1); j++)
            {
                Game2048ChessData chessData = null;
                if (chessboard[j, i] != 0)
                {
                    chessData = new Game2048ChessData(chessboard[j, i]);
                }
                chessboardDic.Add(new Vector2Int(i, j), chessData);
            }
        }
        this.gameState = gameState;
    }


    public void GetBuildData()
    {
        
    }
    public void GetMoveChessData(Vector2Int direction)
    {
       
    }



    private void SwitchState(Game2048GameState gameState)
    {
        this.gameState = gameState;
        switch (this.gameState)
        {
            case Game2048GameState.Begin:
                OnBegin();
                break;
            case Game2048GameState.Build:
                OnBuild();
                break;
            case Game2048GameState.IsGameOver:
                OnIsGameOver();
                break;
            case Game2048GameState.Movie:
                break;
            default:
                break;
        }
    }

    private void OnBegin()
    {
        chessboardDic = new Dictionary<Vector2Int, Game2048ChessData>(Chessboardlength * Chessboardlength);
        for (int i = 0; i < Chessboardlength; i++)
        {
            for (int j = 0; j < Chessboardlength; j++)
            {
                chessboardDic.Add(new Vector2Int(i, j), null);
            }
        }
        SwitchState(Game2048GameState.Build);
    }

    private void OnBuild()
    {
        List<Vector2Int> buildChess = BuildChess;
        foreach (var item in buildChess)
        {
            chessboardDic[item] = new Game2048ChessData();
        }
        SwitchState(Game2048GameState.IsGameOver);
    }

    private void OnIsGameOver()
    {
        if (IsGameOver() == false)
        {
            SwitchState(Game2048GameState.Build);
        }
    }

    private List<Vector2Int> emptyPlace;
    public List<Vector2Int> EmptyPlace
    {
        get
        {
            if (emptyPlace == null)
            {
                emptyPlace = new List<Vector2Int>();
            }
            emptyPlace.Clear();
            foreach (var item in chessboardDic)
            {
                if (item.Value == null)
                {
                    emptyPlace.Add(item.Key);
                }
            }
            return emptyPlace;
        }
    }

    private List<Vector2Int> buildChess;
    public List<Vector2Int> BuildChess
    {
        get
        {
            if (buildChess == null)
            {
                buildChess = new List<Vector2Int>(BuildNumber);
            }
            buildChess.Clear();

            for (int i = 0; i < BuildNumber; i++)
            {
                if (EmptyPlace.Count == 0)
                {
                    break;
                }

                int index = Random.Range(0, EmptyPlace.Count);
                Vector2Int location = EmptyPlace[index];

                buildChess.Add(location);
            }
            return buildChess;
        }
    }

    private Dictionary<Vector2Int, Dictionary<int, List<Vector2Int>>> vectorChessboard;
    /// <summary>
    /// 方向 列数 一堆棋子
    /// </summary>
    public Dictionary<Vector2Int, Dictionary<int, List<Vector2Int>>> VectorChessboard
    {
        get
        {
            if (vectorChessboard == null)
            {
                vectorChessboard = new Dictionary<Vector2Int, Dictionary<int, List<Vector2Int>>>(4);

                Dictionary<int, List<Vector2Int>> up = new Dictionary<int, List<Vector2Int>>(Chessboardlength);
                for (int i = 0; i < Chessboardlength; i++)
                {
                    List<Vector2Int> chessList = new List<Vector2Int>(Chessboardlength);
                    for (int j = 0; j < Chessboardlength; j++)
                    {
                        chessList.Add(new Vector2Int(i, j));
                    }
                    up.Add(i, chessList);
                }
                vectorChessboard.Add(Vector2Int.up, up);

                Dictionary<int, List<Vector2Int>> left = new Dictionary<int, List<Vector2Int>>(Chessboardlength);
                for (int i = 0; i < Chessboardlength; i++)
                {
                    List<Vector2Int> chessList = new List<Vector2Int>(Chessboardlength);
                    for (int j = 0; j < Chessboardlength; j++)
                    {
                        chessList.Add(new Vector2Int(j, i));
                    }
                    left.Add(i, chessList);
                }
                vectorChessboard.Add(Vector2Int.left, left);

                Dictionary<int, List<Vector2Int>> down = new Dictionary<int, List<Vector2Int>>(Chessboardlength);
                for (int i = 0; i < Chessboardlength; i++)
                {
                    List<Vector2Int> chessList = new List<Vector2Int>(Chessboardlength);
                    for (int j = Chessboardlength - 1; j >= 0; j--)
                    {
                        chessList.Add(new Vector2Int(i, j));
                    }
                    down.Add(i, chessList);
                }
                vectorChessboard.Add(Vector2Int.down, down);

                Dictionary<int, List<Vector2Int>> right = new Dictionary<int, List<Vector2Int>>(Chessboardlength);
                for (int i = 0; i < Chessboardlength; i++)
                {
                    List<Vector2Int> chessList = new List<Vector2Int>(Chessboardlength);
                    for (int j = Chessboardlength - 1; j >= 0; j--)
                    {
                        chessList.Add(new Vector2Int(j, i));
                    }
                    right.Add(i, chessList);
                }
                vectorChessboard.Add(Vector2Int.right, right);
            }

            return vectorChessboard;
        }
    }

    private Dictionary<Vector2Int, Dictionary<Vector2Int, List<Vector2Int>>> moviePath;
    /// <summary>
    /// 移动路径 坐标 方向 路径
    /// </summary>
    public Dictionary<Vector2Int, Dictionary<Vector2Int, List<Vector2Int>>> MoviePath
    {
        get
        {
            if (moviePath != null)
            {
                return moviePath;
            }
            moviePath = new Dictionary<Vector2Int, Dictionary<Vector2Int, List<Vector2Int>>>(Chessboardlength * Chessboardlength);

            //添加坐标
            for (int i = 0; i < Chessboardlength; i++)
            {
                for (int j = 0; j < Chessboardlength; j++)
                {
                    Vector2Int coordinate = new Vector2Int(i, j);
                    moviePath.Add(coordinate, new Dictionary<Vector2Int, List<Vector2Int>>(4));
                }
            }

            //添加方向
            foreach (var item in moviePath)
            {
                item.Value.Add(Vector2Int.up, new List<Vector2Int>());
                item.Value.Add(Vector2Int.down, new List<Vector2Int>());
                item.Value.Add(Vector2Int.left, new List<Vector2Int>());
                item.Value.Add(Vector2Int.right, new List<Vector2Int>());
            }

            //添加坐标路径
            foreach (var item in VectorChessboard)
            {
                Vector2Int direction = item.Key;
                foreach (var lines in item.Value)
                {
                    for (int i = 1; i < lines.Value.Count; i++)
                    {
                        Vector2Int coordinate = lines.Value[i];
                        for (int j = i - 1; j >= 0; j--)
                        {
                            moviePath[coordinate][direction].Add(lines.Value[j]);
                        }
                    }
                }
            }

            return moviePath;
        }
    }

    private Dictionary<Vector2Int, List<Vector2Int>> movieOrder;
    /// <summary>
    /// 移动顺序 方向 位置
    /// </summary>
    public Dictionary<Vector2Int, List<Vector2Int>> MovieOrder
    {
        get
        {
            if (movieOrder != null)
            {
                return movieOrder;
            }

            movieOrder = new Dictionary<Vector2Int, List<Vector2Int>>(4);
            movieOrder.Add(Vector2Int.up, new List<Vector2Int>(Chessboardlength * Chessboardlength));
            movieOrder.Add(Vector2Int.down, new List<Vector2Int>(Chessboardlength * Chessboardlength));
            movieOrder.Add(Vector2Int.left, new List<Vector2Int>(Chessboardlength * Chessboardlength));
            movieOrder.Add(Vector2Int.right, new List<Vector2Int>(Chessboardlength * Chessboardlength));

            foreach (var item in VectorChessboard)
            {
                Vector2Int direction = item.Key;
                foreach (var line in item.Value)
                {
                    movieOrder[direction].AddRange(line.Value);
                }
            }

            return movieOrder;
        }
    }

    private int chessboardHashCode;

    private Dictionary<Vector2Int, Dictionary<Vector2Int, Game2048ChessData>> cloneChessboard;
    /// <summary>
    /// 克隆棋盘 方向 坐标 棋子数据
    /// </summary>
    public Dictionary<Vector2Int, Dictionary<Vector2Int, Game2048ChessData>> CloneChessboard
    {
        get
        {
            if (chessboardHashCode == chessboardDic.GetHashCode())
            {
                return cloneChessboard;
            }

            chessboardHashCode = chessboardDic.GetHashCode();

            if (cloneChessboard == null)
            {
                cloneChessboard = new Dictionary<Vector2Int, Dictionary<Vector2Int, Game2048ChessData>>(4);
                cloneChessboard.Add(Vector2Int.up, new Dictionary<Vector2Int, Game2048ChessData>());
                cloneChessboard.Add(Vector2Int.down, new Dictionary<Vector2Int, Game2048ChessData>());
                cloneChessboard.Add(Vector2Int.left, new Dictionary<Vector2Int, Game2048ChessData>());
                cloneChessboard.Add(Vector2Int.right, new Dictionary<Vector2Int, Game2048ChessData>());
            }

            cloneChessboard[Vector2Int.up].Clear();
            cloneChessboard[Vector2Int.down].Clear();
            cloneChessboard[Vector2Int.left].Clear();
            cloneChessboard[Vector2Int.right].Clear();

            foreach (var item in chessboardDic)
            {
                Vector2Int coordinate = item.Key;

                cloneChessboard[Vector2Int.up].Add(coordinate, item.Value != null ? item.Value.Clone() : null);
                cloneChessboard[Vector2Int.down].Add(coordinate, item.Value != null ? item.Value.Clone() : null);
                cloneChessboard[Vector2Int.left].Add(coordinate, item.Value != null ? item.Value.Clone() : null);
                cloneChessboard[Vector2Int.right].Add(coordinate, item.Value != null ? item.Value.Clone() : null);
            }

            return cloneChessboard;
        }
    }

    private bool IsGameOver()
    {
        if (EmptyPlace.Count > 0)
        {
            return false;
        }
        foreach (var item in chessboardDic)
        {
            if (item.Value.Equals(CloneChessboard[Vector2Int.up][item.Key]) == false)
            {
                return false;
            }
            if (item.Value.Equals(CloneChessboard[Vector2Int.down][item.Key]) == false)
            {
                return false;
            }
            if (item.Value.Equals(CloneChessboard[Vector2Int.left][item.Key]) == false)
            {
                return false;
            }
            if (item.Value.Equals(CloneChessboard[Vector2Int.right][item.Key]) == false)
            {
                return false;
            }
        }
        return true;
    }

    private void Movie()
    {
        Movie(Vector2Int.up);
        Movie(Vector2Int.down);
        Movie(Vector2Int.left);
        Movie(Vector2Int.right);

    }
    private void Movie(Vector2Int direction)
    {
        Dictionary<Vector2Int, Game2048ChessData> cloneChessboard = CloneChessboard[direction];

        List<Vector2Int> coordinateMovieOrder = MovieOrder[direction];

        foreach (var item in coordinateMovieOrder)
        {
            List<Vector2Int> path = MoviePath[item][direction];
            Movie(cloneChessboard, item, path);
        }
    }

    private void Movie(Dictionary<Vector2Int, Game2048ChessData> chessboard, Vector2Int current, List<Vector2Int> path)
    {
        Game2048ChessData chessData = chessboard[current];
        if (chessData == null)
        {
            return;
        }

        for (int i = 0; i < path.Count; i++)
        {
            Vector2Int target = path[i];

            Game2048Chessboard.MovieState movieState = chessData.GetMovieState(chessboard[path[i]]);
            switch (movieState)
            {
                case Game2048Chessboard.MovieState.Empty:
                    Debug.LogError("有问题");
                    return;
                case Game2048Chessboard.MovieState.Movie:
                    chessboard[current] = null;
                    chessboard[target] = chessData;
                    current = target;
                    break;
                case Game2048Chessboard.MovieState.Stop:
                    return;
                case Game2048Chessboard.MovieState.Merge:
                    chessData.Merge();

                    chessboard[current] = null;
                    chessboard[target] = chessData;
                    return;
            }
        }
    }
}














