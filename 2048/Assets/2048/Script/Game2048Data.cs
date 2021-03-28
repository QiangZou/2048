using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game2048Data 
{
    public Game2048Data(int length)
    {
        this.length = length;
        board = new int[length, length];
    }

    public int[,] board;
    public int length;



}
