//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Card", menuName = "Card", order = 51)]
public class CardData : ScriptableObject {
    public string name;
    public float cost;
    public MonoScript[] effects;
    public GameObject cardType;

    public float GetCost() {
        return cost;
    }

    public GameObject Instantiate() {
        return Instantiate(cardType);
    }
}
