public class SpeedBoostAbility : PassiveAbility
{
    protected override void PerformAbility()
    {
        var c = transform.parent.gameObject.GetComponent<Creature>();
        c.MoveSpeed += (0.5f);
    }
}
