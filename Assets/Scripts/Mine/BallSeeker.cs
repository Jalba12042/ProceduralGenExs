using UnityEngine;

public class BallSeeker : MonoBehaviour
{
    public GameObject target; // The target to seek (player in this case)
    public float speed = 5f; // Speed of the seeking behavior

    void Update()
    {
        if (target != null)
        {
            // Calculate the direction to the target
            Vector3 direction = (target.transform.position - transform.position).normalized;

            // Move towards the target
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}
