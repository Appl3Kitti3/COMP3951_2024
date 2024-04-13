using UnityEngine;

public class InitPosition : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        GameObject.FindWithTag("Player").transform.position = transform.position;
    }
}
