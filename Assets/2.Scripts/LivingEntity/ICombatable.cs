using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombatable
{
    public abstract int Atk { get; set; }
}

public struct AttackArgs
{
    public ICombatable Attacker;

    // ÃÑÀÇ °æ¿ì
    public Vector3 Origin;
    public Vector3 Direction;
}