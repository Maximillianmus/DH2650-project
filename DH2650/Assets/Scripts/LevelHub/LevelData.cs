using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int Score;
    public float Health;
    public float[] Position;

    public LevelData (GameObject player)
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        Score = Mathf.RoundToInt(Random.Range(0, 3));
        Debug.Log($"Random score: {Score}");
        Health = playerHealth.currentHealth;
        Debug.Log($"Saved health: {Health}");
        Position = new float[3];
        Position[0] = playerMovement.transform.position.x;
        Position[1] = playerMovement.transform.position.y;
        Position[2] = playerMovement.transform.position.z;
    }
}
