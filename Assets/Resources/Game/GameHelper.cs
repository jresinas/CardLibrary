using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameHelper {
    public static void Deal(PlayerController[] players) {
        foreach (PlayerController player in players) {
            foreach (Setup setup in player.setups) {
                if (setup.slot != null) {
                    //setup.slot.OnAdd += OnMove;
                    //setup.slot.OnRemove += OnRemove;
                    foreach (CardData cardData in setup.cards) {
                        if (cardData != null) {
                            Card card = cardData.Instantiate(setup.slot);
                            setup.slot.AddCard(0, card);
                        } else Debug.LogError("Card data could not be found");
                    }
                    //setup.slot.Sort();
                } else Debug.LogError("Slot could not be found");
            }
        }
    }
}
