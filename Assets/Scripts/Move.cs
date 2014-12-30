using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed;
    public Vector2 direction;

    private Vector2 movement;

    void Update()
    {
        movement = new Vector2(direction.x, direction.y) * speed;
        movement = Vector2.ClampMagnitude(movement, speed);
    }

    void FixedUpdate()
    {
        if (!GameManager.Instance.isPaused)
        {
            rigidbody2D.velocity = movement;
        }
        else
        {
            rigidbody2D.velocity = Vector2.zero;
        }
    }
}