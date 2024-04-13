using UnityEngine;

public class ShopToggleController : MonoBehaviour
{

    public void OnAbilityOneToggled()
    {
        var tmp = Player.GetFlag("IFrames");
        Player.ModifyFlag("IFrames", !tmp);
    }
    
    public void OnAbilityTwoToggled()
    {
        var tmp = Player.GetFlag("LDice");
        Player.ModifyFlag("LDice", !tmp);
    }
    
    public void OnAbilityThreeToggled()
    {
        var tmp = Player.GetFlag("BProjectile");
        Player.ModifyFlag("BProjectile", !tmp);
    }
}
