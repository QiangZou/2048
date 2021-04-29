using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZQFramwork;

public class NewBehaviourScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        int[,] chessboard = new int[4, 4]
            {
                { 2,4,2,4},
                { 4,2,4,2},
                {2,4,2,4},
                { 4,2,4,2},
            };

        Game2048ChessboardData data = new Game2048ChessboardData();


        for (int i = 0; i < 100; i++)
        {

        }





        //Dictionary<int, List<Vector2Int>> a1 = data.VectorChessboard[Vector2Int.left];

        //foreach (var item in data.MovieOrder)
        //{
        //    if (item.Key == Vector2Int.up)
        //    {
        //        Debug.Log("up");
        //    }
        //    if (item.Key == Vector2Int.down)
        //    {
        //        Debug.Log("down");
        //    }
        //    if (item.Key == Vector2Int.left)
        //    {
        //        Debug.Log("left");
        //    }
        //    if (item.Key == Vector2Int.right)
        //    {
        //        Debug.Log("right");
        //    }
        //    foreach (var list in item.Value)
        //    {
        //        Debug.Log("路径" + list);
        //    }
        //}


    }

    // Update is called once per frame
    void Update()
    {

    }
}
