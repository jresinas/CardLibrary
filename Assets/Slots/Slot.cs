using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot<T> : MonoBehaviour {
    string name;
    //protected List<CardData> cards = new List<CardData>();
    protected List<T> cards = new List<T>();

    public T foo<T>(T param) {
        return param;
    }

    public void AddCard(T card, int index = 0) {
        cards.Insert(index, card);
    }

    public T RemoveCard(int index = 0) {
        T card = cards[index];
        cards.RemoveAt(index);
        return card;
    }

    public List<T> GetCards() {
        return cards;
    }

    public void Move(Slot<T> destiny, int originIndex = 0, int destinyIndex = 0) {
        T card = this.RemoveCard(originIndex);
        destiny.AddCard(card, destinyIndex);
        //card.Instantiate().OnMove(this, destiny);
    }

    public void Shuffle() {
        System.Random random = new System.Random();
        int n = cards.Count;
        while (n > 1) {
            n--;
            int k = random.Next(n + 1);
            T value = cards[k];
            cards[k] = cards[n];
            cards[n] = value;
        }
    }
}
