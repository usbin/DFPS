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
    //������ ����
    public string Name;
    public Effector Effecter;
}