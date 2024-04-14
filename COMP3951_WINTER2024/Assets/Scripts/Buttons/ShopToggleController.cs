using UnityEngine;

/// <summary>
/// Used for clicking the toggle buttons in the shop scene.
/// There are three methods that correspond to their unique abilities.
///
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 12 2024
/// Source: Applied Unity Skills for OnClick buttons and MonoBehaviour types. 
/// </summary>
public class ShopToggleController : MonoBehaviour
{

    /// <summary>
    /// Sets longer immunity frame status.
    /// </summary>
    public void OnAbilityOneToggled()
    {
        var tmp = Player.GetFlag("IFrames");
        Player.ModifyFlag("IFrames", !tmp);
    }
    
    /// <summary>
    /// Sets lucky dice status.
    /// </summary>
    public void OnAbilityTwoToggled()
    {
        var tmp = Player.GetFlag("LDice");
        Player.ModifyFlag("LDice", !tmp);
    }
    
    /// <summary>
    /// Sets big projectile status.
    /// </summary>
    public void OnAbilityThreeToggled()
    {
        var tmp = Player.GetFlag("BProjectile");
        Player.ModifyFlag("BProjectile", !tmp);
    }
}
