
using System.Threading.Tasks;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // https://www.youtube.com/watch?v=2SXa10ILJms


    public GameObject player;
    public float moveSpeed;
    public Animator animator;

    private float distance;
    
    public Color whiteColor;
    private Vector2 direction;


    // Update is called once per frame
    async void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        direction = player.transform.position - transform.position;
        direction.Normalize();

        /*animator.SetFloat("targetDistance", distance);*/
        if (distance < 5)
        {

            animator.SetFloat("speed", moveSpeed);
            // move the enemy
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            SpriteRenderer rend = GetComponent<SpriteRenderer>();
            rend.color = whiteColor;
            await Task.Run(
            async () =>
            {
                await Task.Delay(500);
            });
            rend.color = Color.white;

            /*transform.rotation = Quaternion.Inverse(transform.rotation);*/
        } else
        {
            animator.SetFloat("speed", -1);
        }
    }

    void FixedUpdate()
    {
        if (direction.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (direction.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
    }
}
