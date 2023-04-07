using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBuff
{
    public abstract string BuffName { get; }
    public abstract string BuffDescription { get; }
    public abstract Sprite BuffImage { get; }
    public abstract float Duration { get; }
    public abstract float RemainDuration { get; }
    public abstract void StartBuff(Player owner);
    public abstract void UpdateBuff(Player owner);
    public abstract void EndBuff(Player owner);
    public abstract bool IsEnd();
}
