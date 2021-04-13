using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : Slot {
    [SerializeField] Transform surface;

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
}
