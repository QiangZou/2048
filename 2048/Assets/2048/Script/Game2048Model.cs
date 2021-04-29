using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZQFramwork;

public class Game2048Model : BaseModel
{
    private Game2048ModelData modelData;
    public Game2048ModelData ModelData
    {
        get
        {
            if (modelData == null)
            {
                modelData = BaseModelData as Game2048ModelData;
            }
            return modelData;
        }
    }

    private Game2048ViewData viewData;
    public Game2048ViewData ViewData
    {
        get
        {
            if (viewData == null)
            {
                viewData = BaseViewData as Game2048ViewData;
            }
            return viewData;
        }
    }

    public Game2048Model(ModuleID moduleID) : base(moduleID)
    {
        ViewData.baseScore = PlayerPrefs.GetInt(BaseScore);
    }


    private const string BaseScore = "Game2048_BaseScore";
    

    



}
