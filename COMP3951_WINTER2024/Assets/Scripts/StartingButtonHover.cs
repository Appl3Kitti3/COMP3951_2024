using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StartingButtonHover : MonoBehaviour
{
    [SerializeField] private GameObject img;

    public void OnMouseOver() {
        img.SetActive(true);
    }

    public void OnMouseExit() {
        img.SetActive(false);
    }
}
