using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePlayerControl
{
    public abstract void Update(PlayerController.ControlArgs args);

    public abstract void ApplyChange();
}
