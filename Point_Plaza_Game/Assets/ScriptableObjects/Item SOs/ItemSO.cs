using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item", menuName ="ScriptableObjects/ItemSO", order = 2)]
public class ItemSO : ScriptableObject
{
    [SerializeField] private string itemName = "Default Item";
    [SerializeField] private int val = 0; 
}
