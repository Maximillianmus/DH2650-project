using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemInteraction : MonoBehaviour
{
    public abstract void Interact(OffHandInteraction offHandInteraction);
}
