using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingChest : MonoBehaviour
{
    public Chest chest;
    private GameObject harpoon;

    // Start is called before the first frame update
    void Start()
    {
        harpoon = GameObject.Find("HarpoonPos");
        harpoon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        SetActiveHarpoon();
    }

    private void SetActiveHarpoon()
    {
        if (chest.IsOpen)
        {
            harpoon.SetActive(true);
        }
    }

}
