using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardText {
    public string fieldName;
    public string text;
}

public class CardData : MonoBehaviour {
    public string name;
    public GameObject cardTemplate;
    public CardText[] cardTexts;


    public Card Instantate() {
        if (cardTemplate != null) {
            GameObject template = Instantiate(cardTemplate);
            GameObject data = Instantiate(gameObject, template.transform);
            CardController card = template.GetComponent<CardController>();
            card.SetData(this);
            return card;
        } else {
            Debug.LogError("Card type undefined");
            return null;
        }
    }

    public Card Instantiate(Slot slot) {
        if (cardTemplate != null) {
            GameObject template = Instantiate(cardTemplate, slot.transform);
            GameObject data = Instantiate(gameObject, template.transform);
            CardController card = template.GetComponent<CardController>();
            card.SetData(this);
            return card;
        } else {
            Debug.LogError("Card type undefined");
            return null;
        }
    }
}
