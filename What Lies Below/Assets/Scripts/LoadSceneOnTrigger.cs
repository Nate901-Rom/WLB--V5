using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnTrigger : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Loading scene: " + sceneToLoad);
            SceneManager.LoadScene(sceneToLoad);
        }

    }

}


