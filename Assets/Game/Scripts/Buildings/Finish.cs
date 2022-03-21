using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : Building
{
    public string sceneControllerTag;

    void Start()
    {
        IsPassable = true;
    }

    public override void Action()
    {
        if (IsNotAction)
        {
            IsNotAction = true;
            GameObject.FindGameObjectWithTag(sceneControllerTag).GetComponent<SceneController>().Win();
        }
    }
}
