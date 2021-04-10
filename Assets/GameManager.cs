using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Setup {
    public Slot slot;
    public CardData[] cards;
}
public class GameManager : MonoBehaviour {
    public Setup[] setups;

    void Awake() {
        foreach (Setup setup in setups) {
            if (setup.slot != null) {
                foreach (CardData cardData in setup.cards) {
                    if (cardData != null) {
                        Card card = cardData.Instantiate(setup.slot);
                        setup.slot.AddCard(card);
                    } else Debug.LogError("Card data could not be found");
                }
                //setup.slot.Sort();
            } else Debug.LogError("Slot could not be found");
        }
    }
}
