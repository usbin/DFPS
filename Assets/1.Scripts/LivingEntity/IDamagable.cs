using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public abstract float Def { get; }
    public void TakeHit(float damage);
}
