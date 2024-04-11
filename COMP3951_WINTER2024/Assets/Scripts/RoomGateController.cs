using UnityEngine;

public class RoomGateController : MonoBehaviour
{
    private AudioSource _sfx;
    // Start is called before the first frame update
    private void Start()
    {
        Room.Instance.EnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        _sfx = transform.parent.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Room.Instance.EnemyCount > 0) return;
        _sfx.Play();
        Destroy(gameObject);



    }
}
