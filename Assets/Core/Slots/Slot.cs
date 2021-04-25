using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enum Permissions {
//    remove, add, order, visible
//}
[System.Serializable]
public class SlotPermissionPhase {
    public string phase;
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
    protected Dictionary<string, List<SlotPermission>> permissionsDict = new Dictionary<string, List<SlotPermission>>();
    public event EventHandler<EventAction> OnAdd;
    public event EventHandler<EventAction> OnRemove;
    public event EventHandler<EventAction> OnMove;

    protected virtual void Awake() {
        foreach (SlotPermissionPhase spp in permissions) permissionsDict[spp.phase] = spp.permission;
    }

    /// <summary>
    /// Add a card to the slot
    /// </summary>
    /// <param name="player">Player who is doing action</param>
    /// <param name="card">Card to add</param>
    /// <param name="index">Position where card will be added (optional)</param>
    /// <returns></returns>
    public bool AddCard(int player, Card card, int index = -1) {
        if (index < 0) index = cards.Count;

        Debug.Log("Request Add");
        if (AllowAdd(player, card)) {
            Debug.Log("Allow Add");
            cards.Insert(index, card);
            card.transform.parent = transform;
            if (OnAdd != null) OnAdd(this, new EventAction(player, card, null, this));
            Sort();

            return true;
        } else return false;
    }

    /// <summary>
    /// Remove a card from the slot
    /// </summary>
    /// <param name="player">Player who is doing action</param>
    /// <param name="card">Card to remove</param>
    /// <returns></returns>
    public bool RemoveCard(int player, Card card) {
        Debug.Log("Request Remove");
        if (AllowRemove(player, card)) {
            Debug.Log("Allow Remove");
            cards.Remove(card);
            card.transform.parent = null;
            if (OnRemove != null) OnRemove(this, new EventAction(player, card, this, null));
            Sort();

            return true;
        } else return false;
    }

    /// <summary>
    /// Remove a card from the slot
    /// </summary>
    /// <param name="player">Player who is doing action</param>
    /// <param name="index">Position of the card to remove</param>
    /// <returns></returns>
    public bool RemoveCard(int player, int index = -1) {
        if (index < 0) index = cards.Count - 1;
        Card card = cards[index];
        return RemoveCard(player, card);
    }

    /// <summary>
    /// Move a card from the slot to another slot
    /// </summary>
    /// <param name="player">Player who is doing action</param>
    /// <param name="card">Card to move</param>
    /// <param name="destiny">Slot of destiny</param>
    /// <returns></returns>
    public bool Move(int player, Card card, Slot destiny) {
        if (AllowMove(player, card, destiny)) {
            if (RemoveCard(player, card)) {
                if (destiny.AddCard(player, card)) {
                    if (OnMove != null) OnMove(this, new EventAction(player, card, this, destiny));
                    return true;
                } else return false;
            } else return false;
        } else return false;
    }

    /// <summary>
    /// Returns all cards of the slot
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Returns true if slot permissions allow to do specific action
    /// </summary>
    /// <param name="player">Player who is doing action</param>
    /// <param name="permission">Type of permission ("Remove", "Add", "Order" or "Flip")</param>
    /// <returns></returns>
    bool GetPermission(int player, string permission) {
        if (player == 0) return true;
        if (!permissionsDict.ContainsKey(Game.currentPhase.name)) return false;

        SlotPermission sp = null;
        foreach (SlotPermission perm in permissionsDict[Game.currentPhase.name]) {
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

    /// <summary>
    /// Returns true if it's allowed to drag cards from this slot
    /// </summary>
    /// <param name="player"></param>
    /// <param name="card"></param>
    /// <returns></returns>
    public virtual bool AllowDrag(int player, Card card) {
        return AllowReorder(player, card) || AllowRemove(player, card);
    }

    /// <summary>
    /// Returns true if it's allowed to reorder cards in this slot
    /// </summary>
    /// <param name="player"></param>
    /// <param name="card"></param>
    /// <returns></returns>
    public virtual bool AllowReorder(int player, Card card) {
        return card != null && GetPermission(player, "Order");
    }

    /// <summary>
    /// Returns true if it's allowed remove cards from this slot
    /// </summary>
    /// <param name="player"></param>
    /// <param name="card"></param>
    /// <returns></returns>
    public virtual bool AllowRemove(int player, Card card) {
        return card != null && GetPermission(player, "Remove");
    }

    /// <summary>
    /// Returns true if it's allowed to add cards to this slot
    /// </summary>
    /// <param name="player"></param>
    /// <param name="card"></param>
    /// <returns></returns>
    public virtual bool AllowAdd(int player, Card card) {
        /*
        return (card != null && allowedTypeCards.Contains(card.GetCardType())) &&
            (true || card.GetSlot() == this && AllowReorder(player, card)) &&
            GetPermission(player, "Add");
        */
        return card != null && GetPermission(player, "Add");
    }

    /// <summary>
    /// Returns true if it's allowed to move cards from this slot to destiny slot
    /// </summary>
    /// <param name="player"></param>
    /// <param name="card"></param>
    /// <param name="destiny"></param>
    /// <returns></returns>
    public virtual bool AllowMove(int player, Card card, Slot destiny) {
        return AllowRemove(player, card) && destiny.AllowAdd(player, card);
    }

    /// <summary>
    /// Returns true if it's allowed to flip cards in this slot
    /// </summary>
    /// <param name="player"></param>
    /// <param name="card"></param>
    /// <returns></returns>
    public virtual bool AllowFlip(int player, Card card) {
        return card != null && GetPermission(player, "Flip");
    }

}
