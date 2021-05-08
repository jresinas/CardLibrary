using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Setup {
    public string name;
    public Slot slot;
    public CardData[] cards;
}
public class PlayerController : MonoBehaviour {
    public int player;
    public Setup[] setups;
    public Dictionary<string, Slot> setupsDict = new Dictionary<string, Slot>();

    void Awake() {
        foreach (Setup setup in setups) setupsDict[setup.name] = setup.slot;
    }

    public void Draw() {
        if (setupsDict.ContainsKey("Deck") && setupsDict.ContainsKey("Hand")) {
            ((Deck)setupsDict["Deck"]).Draw(player, setupsDict["Hand"]);
        }
    }
}
