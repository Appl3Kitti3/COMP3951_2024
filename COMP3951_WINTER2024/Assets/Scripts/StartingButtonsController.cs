using UnityEngine;

/// <summary>
/// Description:
///     Player class is a singleton that represents the current stats of the player. (Damage, Health.)
///         It will have a weapon class to represent its weapon and its playable class to dynamically link
///         and switch to what class should be playing.
/// Author: Teddy Dumam-Ag A01329707
/// Date: March 27 2024
/// Sources: Applied C# and OOP skills.
///
/// </summary>
public class StartingButtonsController : MonoBehaviour
{
    // The three classes 
    public GameObject[] players;
    
    // The cards to pick
    private GameObject[] _cards;
    
    // The GUI used for play button and cards
    private GameObject _gui;
    
    // Called when created
    private void Awake()
    {
        // Set GUI, players list, and cards to false
        _gui = GameObject.Find("GUI");
        _gui.SetActive(false);
        foreach (var v in players)
            v.SetActive(false);
        _cards = GameObject.FindGameObjectsWithTag("CharacterPicker");
        foreach (var v in _cards)
            v.SetActive(false);
    }
    
    // Used for play button onClick
    public void PlayButton(GameObject sender)
    {
        // Disable this button and enable all the cards
        sender.SetActive(false);
        foreach (var v in _cards)
            v.SetActive(true);
    }
    
    // OnClick picking the first card
    public void PickMelee()
    {
        // Enable Melee player gameObject
        players[0].SetActive(true);
        
        // Make MainCamera player gameObject to be this class
        ChangePlayer(players[0]);
        
        // Disable this starting buttons GUI
        CloseChoice();
    }

    // OnClick picking the second card
    public void PickArcher()
    {
        players[1].SetActive(true);
        ChangePlayer(players[1]);
        CloseChoice();
    }

    // OnClick picking the third card
    public void PickMage()
    {
        players[2].SetActive(true);
        ChangePlayer(players[2]);
        CloseChoice();
    }

    // Make Camera player gameObject to the chosen class
    private void ChangePlayer(GameObject o)
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().target = o.transform;
    }

    // Close this GUI
    private void CloseChoice()
    {
        GameObject.Find("StartingButtons").SetActive(false);
        _gui.SetActive(true);
    }
}
