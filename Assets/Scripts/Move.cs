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
        rigidbody2D.velocity = movement;
    }
}