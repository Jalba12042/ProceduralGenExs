using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 5f; // Speed of the ball

    // Update is called once per frame
    void Update()
    {
        // Get player input for movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate the movement direction
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Apply movement to the ball
        GetComponent<Rigidbody>().AddForce(movement * speed);
    }
}
