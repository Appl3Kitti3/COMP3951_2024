using DefaultNamespace;
using UnityEngine;

// TODO: Merges PlayerAttack.cs and PlayerMovement.cs
// No long comments here because it is not finished and ready for use
public class PlayerController : MonoBehaviour
{
    [Header("Player Data")] 
    
    [SerializeField] private int health;

    [SerializeField] private Playable chosenClass;
    // Holds Weapon
    // Weapon holds base damage
    
    [SerializeField] private float walkSpeed = 2f;

    private Rigidbody2D _rigid;

    private Animator _animator;

    private Vector2 _directedMovement;

    void init()
    {
        _rigid = gameObject.GetComponent<Rigidbody2D>();
        _animator = gameObject.GetComponent<Animator>();
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
