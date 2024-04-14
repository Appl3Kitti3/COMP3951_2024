using EndlessCatacombs;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Called on the very start of the game. Represents the functionality and logic of the
/// class picker.
/// 
/// Author: Tedrik "Teddy" Dumam-Ag 
/// Date: March 27 2024
/// Sources: Applied C# and OOP skills.
///
/// </summary>
public class StartingButtonsController : MonoBehaviour
{
    // This is serialized so it can be organized and sorted by (Melee, Archer, and Mage)
    [SerializeField] private GameObject[] players;
    
    // The buttons that represent the class picker.
    [SerializeField] private GameObject[] cards;
    
    // The GUI used for play button and cards
    private GameObject _gui;

    // Used to prevent the player from clicking on a card until they have clicked the play button
    private bool canSelect = false;

    private enum ClassIndex
    {
        Melee = 0,
        Archer = 1,
        Mage = 2
    }
    // Called when created
    private void Awake()
    {
        // Initially set GUI, players list, and cards to false
        _gui = GameObject.Find("GUI");
        _gui.SetActive(false);
    }
    
    // Used for play button onClick
    public void PlayButton(GameObject sender)
    {
        // Disable the play button display and enable the cards
        sender.SetActive(false);
        foreach (var card in cards)
            card.SetActive(true);
        canSelect = true;
    }
    
    // OnClick function for Melee Picker
    public void PickMelee()
    {
        if (canSelect) {
            // Enable Melee player gameObject
            players[(int)ClassIndex.Melee].SetActive(true);
            AssignPlayerComponents(players[(int)ClassIndex.Melee], new Melee());
            // Make MainCamera player gameObject to be this class
            
            // Disable this starting buttons GUI
            CloseChoice();
        }
    }

    // OnClick picking the second card
    public void PickArcher()
    {
        if (canSelect){
            players[(int)ClassIndex.Archer].SetActive(true);
            AssignPlayerComponents(players[(int)ClassIndex.Archer], new Archer());
            CloseChoice();
        }
    }

    // OnClick picking the third card
    public void PickMage()
    {
        if (canSelect) {
            players[(int)ClassIndex.Mage].SetActive(true);
            AssignPlayerComponents(players[(int)ClassIndex.Mage], new Mage());
            CloseChoice();
        }
    }

    // Make Camera player gameObject to the chosen class
    private static void AssignPlayerComponents(GameObject player, Playable c)
    {
        Player.Initialize();
        Player.FixFields(c, player.GetComponent<Animator>());
        ChangePlayer(player);
    }
    
    // Assign chosen player GameObject to the camera so it can follow it
    private static void ChangePlayer(GameObject o /* Assuming o is player*/)
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().target = o.transform;
    }

    // Operates the process after setting up camera and player data. Close GUI and move necessary objects to lobby.
    private void CloseChoice()
    {
        // Remove all the inactive player tagged GameObjects to prevent performance overloading.
        foreach (var player in players)
            if (!player.activeInHierarchy)
                Destroy(player);
        
        // Set the setup canvas active to false.
        GameObject.Find("StartingButtons").SetActive(false);
        
        // Finally, set GUI to true.
        _gui.SetActive(true);


        // Move the necessary objects to lobby.
        Gateway.MoveObjectsToScene(
            1,
            new[]
            {
                _gui,
                GameObject.FindWithTag("Player") /* Gets the active player game object. */,
                GameObject.FindWithTag("CrossHair"),
                GameObject.FindWithTag("MainCamera")
            }
        );

        // Unload this scene.
        SceneManager.UnloadSceneAsync(
            SceneManager.GetSceneByName("Setup")
        );
    }
}
