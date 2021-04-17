using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enum Permissions {
//    remove, add, order, visible
//}
[System.Serializable]
public class SlotPermissionPhase {
    public Phase phase;
    public List<SlotPermission> permission;
}

[System.Serializable]
public class SlotPermission {
    public List<int> players;
    public bool remove;
    public bool add;
    public bool order;
    public bool flip;
}
public class Slot : MonoBehaviour {
    public string name;
    [SerializeField] protected List<string> allowedTypeCards = new List<string>();
    [SerializeField] protected List<Card> cards = new List<Card>();
    [SerializeField] protected List<SlotPermissionPhase> permissions;
    protected Dictionary<Phase, List<SlotPermission>> permissionsDict = new Dictionary<Phase, List<SlotPermission>>();
    public event EventHandler<EventAction> OnAdd;
    public event EventHandler<EventAction> OnRemove;

    protected virtual void Awake() {
        foreach (SlotPermissionPhase spp in permissions) permissionsDict[spp.phase] = spp.permission;
    }

    public bool AddCard(int player, Card card, Slot origin = null, int index = -1) {
        if (index < 0) index = cards.Count;

        Debug.Log("Request Add");
        if (AllowAdd(player, card)) {
            Debug.Log("Allow Add");
            cards.Insert(index, card);
            card.transform.parent = transform;
            if (OnAdd != null) OnAdd(this, new EventAction(player, card, origin, this));
            Sort();

            return true;
        } else return false;
    }

    public bool RemoveCard(int player, Card card, Slot destiny = null) {
        Debug.Log("Request Remove");
        if (AllowRemove(player, card)) {
            Debug.Log("Allow Remove");
            cards.Remove(card);
            card.transform.parent = null;
            if (OnRemove != null) OnRemove(this, new EventAction(player, card, this, destiny));
            Sort();

            return true;
        } else return false;
    }

    public bool RemoveCard(int player, int index = -1, Slot destiny = null) {
        if (index < 0) index = cards.Count - 1;
        Card card = cards[index];
        return RemoveCard(player, card, destiny);
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
        if (!permissionsDict.ContainsKey(Game.phase)) return false;

        SlotPermission sp = null;
        foreach (SlotPermission perm in permissionsDict[Game.phase]) {
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
                case "Flip":
                    return sp.flip;
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

    // Allow flip cards from this slot
    public virtual bool AllowFlip(int player, Card card) {
        return card != null && GetPermission(player, "Flip");
    }

}
