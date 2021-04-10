using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {
    [SerializeField] protected bool flip;
    [SerializeField] protected CardData data;
    // list of players for which card is revealed 
    HashSet<int> visible = new HashSet<int>();
    //public event EventHandler<Slot> BeforeMove;
    //public event EventHandler<Slot> AfterMove;

    // Set CardData
    public void SetData(CardData data) {
        this.data = data;
    }

    // Get type of the card
    public string GetCardType() {
        return data.cardType.name;
    }

    // Get current slot of the card
    public Slot GetSlot() {
        return transform.parent.GetComponent<Slot>();
    }

    public void Reveal(int[] players) {
        visible.UnionWith(players);
    }

    public void Hide() {
        Reveal(new int[] { });
    }

    public void Play<T>(T target) {

    }

    // Move card to slot destiny
    public bool Move(Slot destiny) {
        if (AllowMove(destiny)) {
            Slot origin = GetSlot();
            if (origin.RemoveCard(this)) {
                return destiny.AddCard(this);
            } else return false;
        } else return false;
    }

    // Flip card
    public virtual bool Flip() {
        if (AllowFlip()) {
            flip = !flip;
            return true;
        } else return false;
    }

    public void RightTap() {

    }

    public void LeftTap() {

    }

    public void Invert() {

    }

    public virtual bool AllowMove(Slot destiny) {
        return destiny != null;
    }

    public virtual bool AllowFlip() {
        return true;
    }
}