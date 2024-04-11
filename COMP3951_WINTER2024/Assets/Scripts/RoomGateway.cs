using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomGateway : Gateway
{
    
    protected override void PerformTransitionTask()
    {
        Player.Level++;
    }

    protected override int GetNextScene()
    {
        return Player.Level % 5 == 0 ? Random.Range(9, 11) : // return the range between the last scenes which are the bosses
            Random.Range(6, 9) /*Random.Range(6, 8)*/;
        // start from 6 and there fore
    }

    protected override void PerformMove()
    {
        MoveObjectsToScene(SceneManager.GetSceneAt(SceneManager.sceneCount - 1), MovableGameObj);
    }
}
