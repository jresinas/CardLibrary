using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : Slot {
    [SerializeField] Transform surface;
    public event EventHandler<EventAction> OnDraw;


    //public void Draw(Slot origin, int originIndex = 0, int destinyIndex = 0) {
    //    origin.Move(this, originIndex, destinyIndex);
    //}
    private void Awake() {
        base.Awake();
        //foreach (CardData card in cards) {
        //    card.gameObject.transform.localPosition = Vector3.zero;
        //}
        DrawSurface();
    }

    /*
    public void Draw(int player, Slot hand) {
        CardController card = (CardController)cards[cards.Count - 1];

        if (card.AllowMove(player, hand) && card.AllowFlip(player)) {
            card.Move(player, hand);
            card.Flip(player);
            if (OnDraw != null) OnDraw(this, new EventAction(player, card, this, hand));
        }
    }
    */
    

    public override void Sort() {
        for (int i = 0; i < cards.Count; i++) {
            //cards[i].transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f * i, transform.position.z);
            //cards[i].UpdatePosition(new Vector3(transform.position.x, transform.position.y + 0.01f * (cards.Count - 1 - i), transform.position.z));
            ((CardController)cards[i]).UpdatePosition(new Vector3(transform.position.x, transform.position.y + 0.01f * i, transform.position.z));
        }
    }

    void DrawSurface() {
        surface.localScale = new Vector3(0.1f, 1f, 0.15f);
    }

    /*
    bool AllowDraw(int player, Card card, Slot origin, Slot destiny) {
        return origin.AllowRemove(player, card) && destiny.AllowAdd(player, card);
    }
    */
}
