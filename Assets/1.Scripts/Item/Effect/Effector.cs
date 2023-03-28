using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effector : MonoBehaviour
{
    public abstract void StartEffect(Player owner);
    public abstract void UpdateEffect(Player owner);
    public abstract void EndEffect(Player owner);
    public abstract bool IsEnd();
}
