using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPoint : MonoBehaviour
{
    public string SceneName;
    public LevelPopUp levelPopUp;
    public BoatController boat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (levelPopUp.IsActive()) return;
        if (other.gameObject.tag == "Player" && 
                (boat.agent.remainingDistance < 100 || boat.agent.remainingDistance == float.PositiveInfinity))
        {
            levelPopUp.UpdateLevelInfo(SceneName);
        }
    }
}
