using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomGateway : Gateway
{
    
    protected override void PerformTransitionTask()
    {
        Player.GetInstance().Level++;
    }

    protected override int GetNextScene()
    {
        if (Player.GetInstance().Level % 5 == 0)
            return Random.Range(8, 10); // return the range between the last scenes which are the bosses
        return Random.Range(6, 8);/*Random.Range(6, 8)*/; // start from 6 and there fore
    }

    protected override void PerformMove()
    {
        MoveObjectsToScene(SceneManager.GetSceneAt(SceneManager.sceneCount - 1), movableGameObj);
    }
}
