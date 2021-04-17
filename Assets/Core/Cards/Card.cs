using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {
    //[SerializeField] protected bool flip;
    [SerializeField] protected CardData data;
    // list of players for which card is revealed 
    protected HashSet<int> visible = new HashSet<int>();
    int owner = 1;
    //public event EventHandler<Slot> BeforeMove;
    //public event EventHandler<Slot> AfterMove;

    // Set CardData
    public void SetData(CardData data) {
        this.data = data;
    }

    public void SetVisibility(int[] players) {
        visible = new HashSet<int>(players);
    }

    public void AddVisibility(int[] players) {
        visible.UnionWith(players);
    }

    public void RemoveVisibility(int[] players) {
        visible.ExceptWith(players);
    }

    // Get type of the card
    public string GetCardType() {
        return data.cardType.name;
    }

    // Get current slot of the card
    public Slot GetSlot() {
        return transform.parent.GetComponent<Slot>();
    }

    /*
    public void Reveal(int[] players) {
        visible.UnionWith(players);
    }

    public void Hide() {
        Reveal(new int[] { });
    }
    */

    public void Play<T>(T target) {

    }


/*
    // Move card to slot destiny
    public bool Move(int player, Slot destiny) {
        Debug.Log("Card request move");
        if (AllowMove(player, destiny)) {
            Debug.Log("Card allow move");
            Slot origin = GetSlot();
            if (origin.RemoveCard(player, this, destiny)) {
                return destiny.AddCard(player, this, origin);
            } else return false;
        } else return false;
    }
*/

    /*
    // Flip card
    public virtual bool Flip(int player) {
        if (AllowFlip(player)) {
            flip = !flip;
            return true;
        } else return false;
    }
    */

    public void RightTap(int player) {

    }

    public void LeftTap(int player) {

    }

    public void Invert(int player) {

    }

    public int GetOwner() {
        return owner;
    }

    public void SetOwner(int player) {
        owner = player;
    }

    public bool IsOwned(int player) {
        return owner == player;
    }

    public bool IsVisible(int player) {
        return visible.Contains(player);
    }

    public virtual bool AllowMove(int player, Slot destiny) {
        return destiny != null && destiny.AllowAdd(player, this) && GetSlot() != null && GetSlot().AllowRemove(player, this);
    }

    public virtual bool AllowFlip(int player) {
        return true; //IsVisible(player);
    }
}