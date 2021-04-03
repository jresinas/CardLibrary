using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CardController : Card {
    public CardData card;

    private void Start() {
        MonoScript script = card.effects[0];
        gameObject.AddComponent(script.GetClass());     
    }
}
