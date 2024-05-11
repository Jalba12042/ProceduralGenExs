using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    // Assign the button in the Inspector
    public Button reloadButton;

    void Start()
    {
        // Add a listener for the button click event
        reloadButton.onClick.AddListener(ReloadScene);
    }

    void ReloadScene()
    {
        // Get the index of the current scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Reload the scene by loading it again with its index
        SceneManager.LoadScene(currentSceneIndex);
    }
}
