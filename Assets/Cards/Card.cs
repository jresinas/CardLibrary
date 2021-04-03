using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {
    // list of players for which card is revealed 
    HashSet<int> visible = new HashSet<int>();

    public void Reveal(int[] players) {
        visible.UnionWith(players);
        //OnReveal(players);
    }

    public void Hide() {
        Reveal(new int[] { });
    }

    public void Play(Slot<Card> destiny) {

    }

    public void Move(Slot<Card> destiny) {

    }

    public void RightTap() {

    }

    public void LeftTap() {

    }

    public void Invert() {

    }

    //public void OnMove(Slot<Card> origin, Slot<Card> destiny) {
        
    //}

    //public void OnReveal(int[] players) {

    //}
}
