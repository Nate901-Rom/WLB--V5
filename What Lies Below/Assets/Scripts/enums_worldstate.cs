using UnityEngine;

public class EnumsWorld : MonoBehaviour
{
    public CurrentWorldState currentWorld;

    public enum CurrentWorldState
    {
        NotLoaded = 0,
        Loading = 1,
        Loaded = 2,
        FailedToLoad = -1
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Current world state: " + currentWorld);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
