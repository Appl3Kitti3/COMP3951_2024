using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// https://www.youtube.com/watch?v=GOQV688wbU0
/// </summary>
public class CameraController : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }
}
