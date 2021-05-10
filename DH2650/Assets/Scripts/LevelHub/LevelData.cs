using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class LevelData
{
    public int Score;
    public bool[] ChestsOpenStatus;
    public float Health;
    public float[] Position;

    public LevelData (GameObject player, bool[] chestsOpenStatus)
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

        ChestsOpenStatus = chestsOpenStatus;
        Score = chestsOpenStatus.Count(status => status);
        Debug.Log($"Score: {Score}");
        Health = playerHealth.currentHealth;
        Debug.Log($"Saved health: {Health}");
        Position = new float[3];
        Position[0] = playerMovement.transform.position.x;
        Position[1] = playerMovement.transform.position.y;
        Position[2] = playerMovement.transform.position.z;
    }
}
