using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AvalancheEvent : MonoBehaviour
{
    public MyGrid grid; // Reference to the MyGrid script
    public float avalancheSpeed = 1f; // Speed of the avalanche movement
    public float textureScrollSpeed = 1f; // Speed of the texture scrolling
    public Text countdownText; // Reference to the UI text component for countdown

    private MeshCollider gridMeshCollider; // Mesh collider of the grid
    private Renderer avalancheRenderer; // Reference to the renderer to update texture offset
    private Vector3 initialColliderPosition; // Initial position of the collider
    private float textureOffset = 0f; // Initial texture offset
    private bool countdownStarted = false; // Flag to indicate if countdown has started
    private int countdownTime = 3; // Initial countdown time

    void Start()
    {
        if (grid == null)
        {
            Debug.LogError("MyGrid reference not set!");
            return;
        }

        gridMeshCollider = grid.GetComponent<MeshCollider>();

        if (gridMeshCollider == null)
        {
            Debug.LogError("MeshCollider component not found on MyGrid!");
            return;
        }

        avalancheRenderer = GetComponent<Renderer>();

        if (avalancheRenderer == null)
        {
            Debug.LogError("Renderer component not found on AvalancheEvent GameObject!");
            return;
        }

        // Store the initial position of the collider
        initialColliderPosition = transform.position;
    }

    void Update()
    {
        // Move the collider down the slope
        MoveCollider();

        // Scroll the texture
        ScrollTexture();

        // Start the countdown when the collider is moving
        if (!countdownStarted && transform.position.y > grid.transform.position.y)
        {
            StartCoroutine(StartCountdown());
            countdownStarted = true;
        }
    }

    void MoveCollider()
    {
        // Check if the avalanche collider has reached the bottom of the grid
        if (transform.position.y <= grid.transform.position.y)
        {
            // Reset the avalanche
            ResetAvalanche();
            return;
        }

        // Move the collider along the slope direction
        transform.position -= Vector3.up * avalancheSpeed * Time.deltaTime;
    }

    void ScrollTexture()
    {
        // Calculate texture offset based on time
        textureOffset += textureScrollSpeed * Time.deltaTime;

        // Apply the texture offset
        avalancheRenderer.material.mainTextureOffset = new Vector2(0f, textureOffset);
    }

    // Reset the collider position and texture offset
    public void ResetAvalanche()
    {
        transform.position = initialColliderPosition;
        textureOffset = 0f;
        avalancheRenderer.material.mainTextureOffset = Vector2.zero;
        countdownStarted = false;
    }

    // Coroutine to start the countdown
    IEnumerator StartCountdown()
    {
        while (countdownTime > 0)
        {
            countdownText.text = "Avalanche! Take cover in " + countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }
        countdownText.text = "Avalanche! Take cover!";
    }
}
