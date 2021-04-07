using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZQFramwork;

public class Game2048Model : BaseModel
{
    private const string BaseScore = "Game2048_BaseScore";

    public Game2048Model(ModuleID moduleID) : base(moduleID)
    {
        (BaseViewData as Game2048ViewData).baseScore = PlayerPrefs.GetInt(BaseScore);
    }



}
