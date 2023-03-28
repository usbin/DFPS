using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Item : MonoBehaviour
{
    public ItemData ItemData;


    
}


[System.Serializable]
public struct ItemData
{
    //아이템 정보
    public string Name;
    public Effector Effecter;
}