using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerControl
{
    public abstract void Update(PlayerController.ControlArgs args);

    public abstract void ApplyChange();
}
