using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BallShooter : MonoBehaviour
{
    public GameObject ball;
    public GameObject player;
    public float countdownDuration = 3f;
    public Text countdownText;
    public float timeBetweenRuns = 10f; // Time between each run of the BallShooter script

    private bool isCountingDown = false;

    void Start()
    {
        countdownText.text = "";
        StartCoroutine(ShootRoutine());
    }

    IEnumerator ShootRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenRuns);
            if (!isCountingDown)
            {
                isCountingDown = true;
                StartCoroutine(ShootCountdown());
            }
        }
    }

    IEnumerator ShootCountdown()
    {
        countdownText.text = "Seek cover! Avalanche incoming!";
        yield return new WaitForSeconds(1f);
        countdownText.text = "3";
        yield return new WaitForSeconds(1f);
        countdownText.text = "2";
        yield return new WaitForSeconds(1f);
        countdownText.text = "1";
        yield return new WaitForSeconds(1f);
        countdownText.text = "";

        ShootBall();
        isCountingDown = false;
    }

    void ShootBall()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Instantiate(ball, transform.position, Quaternion.LookRotation(direction));
    }
}
