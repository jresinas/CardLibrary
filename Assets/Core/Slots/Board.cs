using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board: Slot {
    float CARD_SPACE = 1f;
    [SerializeField] Transform surface;
    [SerializeField] Vector2 size;

    void Awake() {
        base.Awake();
        //foreach (CardData card in cards) {
        //    card.gameObject.transform.localPosition = Vector3.zero;
        //}
        DrawSurface();
    }

    public override void Sort() {
        for (int i = 0; i < cards.Count; i++) {
            //cards[i].transform.position = new Vector3(transform.position.x - (size.x-CARD_SPACE)/2 + CARD_SPACE * i, transform.position.y, transform.position.z);
            //cards[i].UpdatePosition(new Vector3(transform.position.x - (size.x - CARD_SPACE) / 2 + CARD_SPACE * (cards.Count - 1 - i), transform.position.y, transform.position.z));
            ((CardController)cards[i]).UpdatePosition(new Vector3(transform.position.x - (size.x - CARD_SPACE) / 2 + CARD_SPACE * i, transform.position.y, transform.position.z));
        }
    }

    /// <summary>
    /// Returns the size of the board
    /// </summary>
    /// <returns></returns>
    public Vector2 GetSize() {
        return size;
    }

    /// <summary>
    /// Set the size of the board
    /// </summary>
    /// <param name="size"></param>
    public void SetSize(Vector2 size) {
        this.size = size;
        DrawSurface();
        //Sort();
    }

    /// <summary>
    /// Draw the board surface
    /// </summary>
    void DrawSurface() {
        surface.localScale = new Vector3(size.x * 0.1f, 1f, size.y * 0.15f);
    }

    public override bool AllowAdd(int player, Card card) {
        return base.AllowAdd(player, card) && cards.Count < size.x;
    }
}
