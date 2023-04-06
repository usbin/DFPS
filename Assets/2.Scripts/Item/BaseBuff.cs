using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBuff
{
    public abstract Sprite BuffImage { get; }
    public abstract void StartBuff(Player owner);
    public abstract void UpdateBuff(Player owner);
    public abstract void EndBuff(Player owner);
    public abstract bool IsEnd();
}
