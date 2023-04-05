using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class BaseItem : MonoBehaviour
{
    public ItemData ItemData;
    public abstract BaseBuff Buff { get; }
}


[System.Serializable]
public struct ItemData
{
    //아이템 정보
    public string Name;
}