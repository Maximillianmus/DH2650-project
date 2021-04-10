using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ContainerInteraction : MonoBehaviour
{
    public abstract void Interact(GameObject item, OffHandInteraction offHandInteraction);   
}
