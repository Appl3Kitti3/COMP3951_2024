using UnityEngine;

/// <summary>
/// Description:
///     Partial class of PlayerController. Represents the movement functions of the player.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 2 2024 (Revision 04/12/2024)
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
    // Gets the id name of the speed parameter of its animator
    private static readonly int Speed = Animator.StringToHash("Speed");

    // Gets the speed of the player. If player does not have an ultimate active, get its normal speed, otherwise return the reduced ultimate speed.
    private int GetSpeed => _hasUltimateActive ? Player.GetUltSpeed : Player.GetSpeed;

    /// <summary>
    /// Sets the animator speed parameter of the players x and y input direction.
    /// </summary>
    private void GetAxisPositionValues()
    {
        // Move logic of the player.
        _directedMovement.x = Input.GetAxisRaw("Horizontal");
        _directedMovement.y = Input.GetAxisRaw("Vertical");
        _animator.SetFloat(Speed, _directedMovement.sqrMagnitude);
    }

    // Flip the player, based on their movement.
    private void FlipSelf()
    {
        // Flip image
        if ((int)_directedMovement.x != 0)
            transform.localScale = Utility.GetInverseScale(transform.localScale, (int)_directedMovement.x);
    }

    // Calculation of moving the player.
    private void MovePlayer()
    {
        // Player movement
        _rigid.MovePosition(_rigid.position + 
                            _directedMovement * (GetSpeed * Time.deltaTime));
    }
}
