
public class Ability : Attack
{

    protected override void Init()
    {
        ParentObject = transform.parent.gameObject;
    }
}

