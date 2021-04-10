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

    public Card Instantiate(Slot slot) {
        if (cardType != null) {
            GameObject obj = Instantiate(cardType, slot.transform);
            CardController card = obj.GetComponent<CardController>();
            card.SetData(this);
            return card;
        } else {
            Debug.LogError("Card type undefined");
            return null;
        }
    }
}
