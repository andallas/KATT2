using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    private Vector2 movement;

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        movement = new Vector2(inputX, inputY) * speed;
        movement = Vector2.ClampMagnitude(movement, speed);
    }

    void FixedUpdate()
    {
        rigidbody2D.velocity = movement;
    }
}