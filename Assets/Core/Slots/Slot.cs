using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enum Permissions {
//    remove, add, order, visible
//}

[System.Serializable]
public class SlotPermission {
    public List<int> players;
    public bool remove;
    public bool add;
    public bool order;
    public bool visible;
}
public class Slot : MonoBehaviour {
    string name;
    [SerializeField] protected List<string> allowedTypeCards = new List<string>();
    [SerializeField] protected List<Card> cards = new List<Card>();
    [SerializeField] protected SlotPermission[] permissions;

    public bool AddCard(int player, Card card, int index = -1) {
        if (index < 0) index = cards.Count;

        Debug.Log("Request Add");
        if (AllowAdd(player, card)) {
            Debug.Log("Allow Add");
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

    public bool RemoveCard(int player, Card card) {
        if (AllowRemove(player, card)) {
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

    /*
    SlotPermission GetPermission(int player) {
        foreach (SlotPermission permission in permissions) {
            if (permission.players.Contains(player)) return permission;
        }
        return null;
    }
    */

    bool GetPermission(int player, string permission) {
        if (player == 0) return true;

        SlotPermission sp = null;
        foreach (SlotPermission perm in permissions) {
            if (perm.players.Contains(player)) sp = perm;
        }

        if (sp != null) {
            switch (permission) {
                case "Remove":
                    return sp.remove;
                case "Add":
                    return sp.add;
                case "Order":
                    return sp.order;
                case "Visible":
                    return sp.visible;
            }
        }
        return false;
    }

    public virtual void Sort() { }

    // Allow starting drag cards from this slot
    public virtual bool AllowDrag(int player, Card card) {
        return AllowReorder(player, card) || AllowRemove(player, card);
    }

    // Allow reorder cards from this slot
    public virtual bool AllowReorder(int player, Card card) {
        return card != null && GetPermission(player, "Order");
    }

    // Allow remove cards from this slot
    public virtual bool AllowRemove(int player, Card card) {
        return card != null && GetPermission(player, "Remove");
    }

    // Allow add cards to this slot
    public virtual bool AllowAdd(int player, Card card) {
        /*
        return (card != null && allowedTypeCards.Contains(card.GetCardType())) &&
            (true || card.GetSlot() == this && AllowReorder(player, card)) &&
            GetPermission(player, "Add");
        */
        return card != null && GetPermission(player, "Add");
    }

}
