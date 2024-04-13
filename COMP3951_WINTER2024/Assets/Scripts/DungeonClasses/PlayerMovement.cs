using UnityEngine;

/// <summary>
/// Description:
///     Partial class of PlayerController. Represents the movement functions of the player.
/// Author: 
/// Date: April 2 2024
/// Sources:
///
///     Tutorial on 2D Top Down movement.
///     https://www.youtube.com/watch?v=u8tot-X_RBI
///
///     Tutorial on 2D Top Down movement alternative.
///     https://www.youtube.com/watch?v=whzomFgjT50
///
///     Tutorial on how to flip a sprite. (Line 70)
///     https://www.youtube.com/watch?v=Cr-j7EoM8bg&t=140s
///
///     Tutorial on collisions
///     https://www.youtube.com/watch?v=YSzmCf_L2cE
/// </summary>
partial class PlayerController
{
    private static readonly int Speed = Animator.StringToHash("Speed");

    void GetAxisPositionValues()
    {
        // Move logic of the player.
        _directedMovement.x = Input.GetAxisRaw("Horizontal");
        _directedMovement.y = Input.GetAxisRaw("Vertical");
        _animator.SetFloat(Speed, _directedMovement.sqrMagnitude);
    }
    
    void FlipSelf()
    {
        // Flip image
        if (_directedMovement.x < 0)
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        else if (_directedMovement.x > 0)
            gameObject.transform.localScale = new Vector3(1, 1, 1);
    }
    
    void MovePlayer()
    {
        // Player movement
        _rigid.MovePosition(_rigid.position + 
                            _directedMovement * (Player.ChosenClass.GetSpeed() * Time.deltaTime));
    }
}
