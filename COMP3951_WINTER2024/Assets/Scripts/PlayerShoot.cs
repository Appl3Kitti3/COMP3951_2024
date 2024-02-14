using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerShoot : MonoBehaviour
{

    // https://www.youtube.com/watch?v=mgjWA2mxLfI
    public Camera sceneCamera;
    public Animator animator;

    // https://www.youtube.com/watch?v=77dWGDFqcps

    // Update is called once per frame
    void Update()
    {

        // https://www.youtube.com/watch?v=mgjWA2mxLfI
        if (Input.GetMouseButton(0))
            animator.SetTrigger("Primary");
        else if (Input.GetMouseButton(1))
            animator.SetTrigger("Ultimate");







    }

    
}
