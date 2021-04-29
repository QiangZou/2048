using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZQFramwork;

public class Game2048Chessboard : BaseView
{
    public Game2048Chess chessClone;

    private GameObjectPool pool;

    private const int chessboardlength = 4;
    private Game2048GameState gameState;
    private Dictionary<Vector2Int, Transform> chessboardTransform;
    private Dictionary<Vector2Int, Game2048Chess> chessboardDic = new Dictionary<Vector2Int, Game2048Chess>();


    protected override void Awake()
    {
        Image[] chessTransform = transform.GetComponentsInChildren<Image>();

        chessboardTransform = new Dictionary<Vector2Int, Transform>(chessboardlength * chessboardlength);

        for (int i = 0; i < chessboardlength; i++)
        {
            for (int j = 0; j < chessboardlength; j++)
            {
                chessboardTransform.Add(new Vector2Int(i, j), chessTransform[i * chessboardlength + j].transform);
            }
        }


        pool = GameObjectPool.CreatePool(chessClone.gameObject);
    }

    protected override void Start()
    {
        NewGame();
    }

    public void Init()
    {
        chessboardDic = new Dictionary<Vector2Int, Game2048Chess>(chessboardlength * chessboardlength);
        for (int i = 0; i < chessboardlength; i++)
        {
            for (int j = 0; j < chessboardlength; j++)
            {
                chessboardDic.Add(new Vector2Int(i, j), null);
            }
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

    public void Spawn()
    {
        if (EmptyPlace.Count == 0)
        {
            Debug.LogError("生成错误棋盘没有空的位置");
            return;
        }

        for (int i = 0; i < 2; i++)
        {
            DebugEmptyPlace();
            int index = Random.Range(0, EmptyPlace.Count);
            Vector2Int location = EmptyPlace[index];
            Spawn(location, Game2048ChessType.Number_2);
            //只能生成一个棋子
            if (EmptyPlace.Count == 0)
            {
                break;
            }
        }
    }

    private void DebugEmptyPlace()
    {
        Debug.Log("棋盘空的数量：" + EmptyPlace.Count);
        Debug.Log("棋盘空的位置：" + EmptyPlace.GetString());

    }

    public void Spawn(Vector2Int location, Game2048ChessType chessType)
    {
        if (chessboardDic.ContainsKey(location) == false)
        {
            Debug.LogError("生成位置错误location : " + location.ToString());
            return;
        }
        if (chessboardDic[location] != null)
        {
            Debug.LogError("生成位置错误 该位置有棋子location : " + location.ToString());
            return;
        }

        Debug.Log("生成位置：" + location.ToString());
        Debug.Log("生成值：" + ((int)chessType).ToString());

        Game2048Chess chess = pool.Get<Game2048Chess>();
        chess.Init(Game2048ChessType.Number_2);
        chessboardDic[location] = chess;
        RefreshPosition();

        Debug.Log("棋盘状态");
        Debug.Log(chessboardDic.GetString());
    }

    private void RefreshPosition()
    {
        foreach (var item in chessboardDic)
        {
            if (item.Value != null)
            {
                item.Value.transform.position = chessboardTransform[item.Key].position;
            }
        }
    }

    private void NewGame()
    {
        gameState = Game2048GameState.Begin;
        SwitchState();
    }

    private void SwitchState()
    {
        Debug.Log("当前游戏状态：" + gameState.ToString());
        switch (gameState)
        {
            case Game2048GameState.Begin:
                Init();
                gameState = Game2048GameState.Build;
                SwitchState();
                break;
            case Game2048GameState.Build:
                Spawn();
                VPTimer.In(0.5f, () =>
                {
                    SwitchState();
                });
                break;
            case Game2048GameState.Movie:
                break;
            case Game2048GameState.IsGameOver:
                break;
            default:
                break;
        }
    }

    protected override void Update()
    {
        base.Update();
        if (gameState != Game2048GameState.Build)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("您按下了W键");
            Movie(Vector2Int.up);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("您按下了S键");
            Movie(Vector2Int.down);

        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("您按下了A键");
            Movie(Vector2Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("您按下了D键");
            Movie(Vector2Int.right);
        }
    }

    List<Vector2Int> chessList = new List<Vector2Int>(chessboardlength);
    private List<Vector2Int> GetChessList(Vector2Int vector, int index)
    {
        chessList.Clear();

        if (vector == Vector2Int.up || vector == Vector2Int.left)
        {
            for (int i = 0; i < chessboardlength; i++)
            {
                if (vector == Vector2Int.up)
                {
                    chessList.Add(new Vector2Int(i, index));
                }
                else if (vector == Vector2Int.left)
                {
                    chessList.Add(new Vector2Int(index, i));
                }
            }
        }
        else if (vector == Vector2Int.down || vector == Vector2Int.right)
        {
            for (int i = chessboardlength - 1; i >= 0; i--)
            {
                if (vector == Vector2Int.down)
                {
                    chessList.Add(new Vector2Int(i, index));
                }
                else if (vector == Vector2Int.right)
                {
                    chessList.Add(new Vector2Int(index, i));
                }
            }
        }

        return chessList;
    }

    private void SetGetChessList(Vector2Int vector, int index, List<Game2048Chess> chessList)
    {
        if (vector == Vector2Int.up || vector == Vector2Int.left)
        {
            for (int i = 0; i < chessList.Count; i++)
            {
                if (vector == Vector2Int.up)
                {
                    chessboardDic[new Vector2Int(i, index)] = chessList[i];
                }
                else if (vector == Vector2Int.left)
                {
                    chessboardDic[new Vector2Int(index, i)] = chessList[i];
                }
            }
        }
        else if (vector == Vector2Int.down || vector == Vector2Int.right)
        {
            for (int i = chessList.Count - 1; i >= 0; i--)
            {
                if (vector == Vector2Int.down)
                {
                    chessboardDic[new Vector2Int(i, index)] = chessList[i];
                    chessList.Add(chessboardDic[new Vector2Int(index, i)]);
                }
                else if (vector == Vector2Int.right)
                {
                    chessboardDic[new Vector2Int(index, i)] = chessList[i];
                }
            }
        }
    }

    private void Movie(Vector2Int vector)
    {
        foreach (var item in chessboardDic)
        {
            if (item.Value != null)
            {
                item.Value.isMerge = false;
            }
        }

        for (int i = 0; i < chessboardlength; i++)
        {
            List<Vector2Int> chessList = GetChessList(vector, i);
            Debug.Log("获取棋盘一列数据");
            Debug.Log(chessList.GetString());
            Movie(chessList);
            Debug.Log("一列数据移动后");
            Debug.Log(chessboardDic.GetString());
        }
        Movie();
        VPTimer.In(0.4f, () =>
        {
            gameState = Game2048GameState.Build;
            SwitchState();
        });

        //Debug.Log(chessboardDic.GetString());
    }

    private void Movie()
    {
        foreach (var item in chessboardDic)
        {
            if (item.Value != null)
            {
                Transform transform = chessboardTransform[item.Key];
                item.Value.Movie(transform);
            }
        }
    }

    public enum MovieState
    {
        Empty,
        /// <summary>
        /// 移动
        /// </summary>
        Movie,
        /// <summary>
        /// 停止
        /// </summary>
        Stop,
        /// <summary>
        /// 合并
        /// </summary>
        Merge,
    }


    private void Movie(List<Vector2Int> chessList)
    {
        for (int i = 1; i < chessList.Count; i++)
        {
            if (chessboardDic[chessList[i]] == null)
            {
                continue;
            }
            Game2048Chess current = chessboardDic[chessList[i]];
            chessboardDic[chessList[i]] = null;

            Movie(chessList, i, current);
        }
    }

    private void Movie(List<Vector2Int> chessList, int index, Game2048Chess chess)
    {
        for (int i = index - 1; i >= 0; i--)
        {
            Vector2Int current = chessList[i + 1];
            Vector2Int target = chessList[i];

            MovieState movieState = Movie(chess, current, target);

            if (movieState != MovieState.Movie)
            {
                break;
            }
        }
    }

    private MovieState Movie(Game2048Chess chess, Vector2Int current, Vector2Int target)
    {
        if (chessboardDic[target] == null)
        {
            chessboardDic[current] = null;
            chessboardDic[target] = chess;
            return MovieState.Movie;
        }
        if (chessboardDic[target].value != chess.value)
        {
            chessboardDic[current] = chess;
            return MovieState.Stop;
        }
        if (chessboardDic[target].isMerge)
        {
            chessboardDic[current] = chess;
            return MovieState.Stop;
        }
        if (chessboardDic[target].value == chess.value)
        {
            pool.Recycle(chessboardDic[target].gameObject);//回收
            chess.isMerge = true;
            chess.value = (Game2048ChessType)((int)chess.value * 2);

            chessboardDic[current] = null;
            chessboardDic[target] = chess;
            return MovieState.Merge;
        }
        return MovieState.Empty;
    }
}
