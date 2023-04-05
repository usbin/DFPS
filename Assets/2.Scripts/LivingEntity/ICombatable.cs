using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombatable
{
    public abstract GameObject GameObject {get;}
    public abstract int Atk { get; set; }
    public abstract int AtkSpeed { get; set; }
    public abstract int Def { get; set; }

    public abstract void TakeHit(int damage);
    public abstract void Recover(int amount);
}


