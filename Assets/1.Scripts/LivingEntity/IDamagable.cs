using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public abstract int Def { get; set; }
    public void TakeHit(int damage);
}
