
using UnityEngine;

public class LevelCompletionTrigger : MonoBehaviour
{
    public MazeManager mazeManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mazeManager.GenerateMaze();
        }
    }
}

