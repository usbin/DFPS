using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombatable
{
    public abstract int Atk { get; set; }
    public abstract int AtkSpeed { get; set; }
    public abstract int Def { get; set; }
    public void TakeHit(int damage, AttackArgs attackArgs);
}

public struct AttackArgs
{
    public ICombatable Attacker;
    public ICombatable Defender;
    

    // ÃÑÀÇ °æ¿ì
    public Vector3 Origin;
    public Vector3 Direction;
}