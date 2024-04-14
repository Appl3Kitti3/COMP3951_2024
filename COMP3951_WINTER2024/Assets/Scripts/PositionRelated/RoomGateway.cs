using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///     Used to perform the task of leveling up the player in each dungeon room progression. And determines on doing the random number generator logic
/// for which room should appear next.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 13 2024
/// Source: Applied C# and Unity Skills
/// </summary>
public class RoomGateway : Gateway
{
    protected override void PerformTransitionTask()
    {
        Player.Level++;
    }

    protected override int GetNextScene()
    {
        return Player.Level % 5 == 0 ? Random.Range(9, 11) : // return the range between the last scenes which are the bosses
            Random.Range(6, 9);
    }

    /// <summary>
    /// Moves the movable objects to the new loaded room scene. This needs to happen because when there are duplicate room cases. And the program
    /// might accidentally think of the first room as the new room and once the previous room unloads, it does not bring the objects to the next scene.
    /// </summary>
    protected override void PerformMove()
    {
        MoveObjectsToScene(SceneManager.GetSceneAt(SceneManager.sceneCount - 1), MovableGameObj);
    }
}
