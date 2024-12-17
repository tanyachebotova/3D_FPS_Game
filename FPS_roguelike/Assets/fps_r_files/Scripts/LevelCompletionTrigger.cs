
using UnityEngine;

public class LevelCompletionTrigger : MonoBehaviour
{
    public MazeManager mazeManager;
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mazeManager.GenerateNextMazeBlock();
            PlayerMove playerMove = player.GetComponent<PlayerMove>();
            if (playerMove != null)
            {
                playerMove.grenadesRemaining = playerMove.grenadesPerBlock;
                Debug.Log("Grenades replenished! Current grenades: " + playerMove.grenadesRemaining);
            }
            else
            {
                Debug.LogError("PlayerMove component not found on the player object!");
            }

        }
    }
}

