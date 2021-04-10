using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour {
    string name;
    [SerializeField] protected List<string> allowedTypeCards = new List<string>();
    [SerializeField] protected List<Card> cards = new List<Card>();

    public bool AddCard(Card card, int index = -1) {
        if (index < 0) index = cards.Count;

        if (AllowAdd(card)) {
            cards.Insert(index, card);
            card.transform.parent = transform;
            Sort();
            return true;
        } else return false;
    }

    //public Card RemoveCard(int index = -1) {
    //    if (index < 0) index = cards.Count - 1;
    //    Card card = cards[index];
    //    return RemoveCard(card);
    //}

    public bool RemoveCard(Card card) {
        if (AllowRemove(card)) {
            cards.Remove(card);
            card.transform.parent = null;
            Sort();
            return true;
        } else return false;
    }

    public List<Card> GetCards() {
        return cards;
    }

    /*
    public bool Move(Slot destiny, int originIndex = -1, int destinyIndex = -1) {
        if (originIndex < 0) originIndex = cards.Count - 1;
        Card card = cards[originIndex];
        return Move(destiny, card, destinyIndex);
        //Card card = this.RemoveCard(originIndex);
        //destiny.AddCard(card, destinyIndex);
        //card.OnMove(this, destiny);
    }
    */

    /*
    public bool Move(Slot destiny, Card card, int destinyIndex = -1) {
        if (this.AllowRemove(card) && destiny.AllowAdd(card)) {
            this.RemoveCard(card);
            return destiny.AddCard(card, destinyIndex);
            //card.OnMove(this, destiny);
        } else {
            Debug.Log("Move not valid");
            return false;
        }
    }
    */

    public void Shuffle() {
        System.Random random = new System.Random();
        int n = cards.Count;
        while (n > 1) {
            n--;
            int k = random.Next(n + 1);
            Card value = cards[k];
            cards[k] = cards[n];
            cards[n] = value;
        }
    }

    public virtual void Sort() { }

    // Allow starting drag cards from this slot
    public virtual bool AllowDrag(Card card) {
        return AllowReorder(card) || AllowRemove(card);
    }

    // Allow reorder cards from this slot
    public virtual bool AllowReorder(Card card) {
        return card != null;
    }

    // Allow remove cards from this slot
    public virtual bool AllowRemove(Card card) {
        return card != null;
    }

    // Allow add cards to this slot
    public virtual bool AllowAdd(Card card) {
        return (card != null && allowedTypeCards.Contains(card.GetCardType())) &&
            (true || card.GetSlot() == this && AllowReorder(card));
    }

}
