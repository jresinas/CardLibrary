using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMouse : MonoBehaviour {
    static int BUTTONS = 2;
    float PRESS_THRESHOLD = 0.15f;
    /// <summary>
    /// <br>0: click up/hold events return selectedCard only when click up/hold card is the same where clicked</br>
    /// <br>1: click up/hold events return selectedCard where clicked</br>
    /// <br>2: click up/hold events return selectedCard where click up/hold</br>
    /// </summary>
    int MODE = 2;
    
    //[SerializeField] protected int state = 0;
    [SerializeField] bool[] hold = new bool[BUTTONS];
    [SerializeField] float[] press = new float[BUTTONS];
    [SerializeField] bool[] drag = new bool[BUTTONS];
    [SerializeField] CardController[] selectedCard = new CardController[BUTTONS];

    public virtual event EventHandler<InputEventData> OnClick;
    public virtual event EventHandler<InputEventData> OnClickUp;
    public virtual event EventHandler<InputEventData> OnEnterHold;
    public virtual event EventHandler<InputEventData> OnHold;
    public virtual event EventHandler<InputEventData> OnExitHold;

    //protected virtual void ChangeState(int current, int next) { }
    
    void Update() {
        for (int button = 0; button < BUTTONS; button++) ButtonBehaviour(button);
    }

    void ButtonBehaviour(int button) {
        Slot targetSlot = GetTarget<Slot>();
        CardController targetCard = GetTarget<CardController>();

        if (Input.GetButtonDown("Button" + button)) {
            selectedCard[button] = GetCard();
            if (OnClick != null) OnClick(this, new InputEventData(button, selectedCard[button], targetSlot, targetCard));
        }

        if (Input.GetButtonUp("Button" + button)) {
            if (hold[button]) {
                hold[button] = false;
                if (OnExitHold != null) OnExitHold(this, new InputEventData(button, selectedCard[button], targetSlot, targetCard));
            //} else if (card == null || (card != null && card == selectedCard[button])) {
            } else {
                CardController card = GetCardByMode(selectedCard[button]);
                if (OnClickUp != null) OnClickUp(this, new InputEventData(button, card, targetSlot, targetCard));
            }
            selectedCard[button] = null;
        }

        if (Input.GetButton("Button" + button)) {
            //CardController card = GetCard();
            CardController card = GetCardByMode(selectedCard[button]);
            if (card != null && card == selectedCard[button]) press[button] += Time.deltaTime;
            else press[button] = 0;
        } else {
            press[button] = 0;
        }

        if (press[button] > PRESS_THRESHOLD && !hold[button]) {
            if (OnEnterHold != null) OnEnterHold(this, new InputEventData(button, selectedCard[button], targetSlot, targetCard));
            hold[button] = true;
        }

        if (hold[button]) {
            if (OnHold != null) OnHold(this, new InputEventData(button, selectedCard[button], targetSlot, targetCard));
        }
    }

    /// <summary>
    /// Returns the card below mouse pointer
    /// </summary>
    /// <returns></returns>
    CardController GetCard() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.tag == "Card") {
                return hit.collider.GetComponentInParent<CardController>();
            }
        }
        return null;
    }

    /// <summary>
    /// Returns the object of type T below mouse pointer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    T GetTarget<T>() {
        Card currentCard = GetCard();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        foreach (RaycastHit hit in hits) {
            if (hit.collider.tag == typeof(T).ToString()) {
                T target = hit.collider.GetComponentInParent<T>();
                // if T = Card, ensure that target isn't the selectedCard
                if (!ReferenceEquals(target, currentCard)) return target;
            }
        }
        return default(T);
    }

    CardController GetCardByMode(CardController selectedCard) {
        CardController card = GetCard();
        switch (MODE) {
            case 0:
                if (card != selectedCard) card = null;
                break;
            case 1:
                card = selectedCard;
                break;
            case 2:
                break;
        }

        return card;
    }

    /*
    public void ClearSelectedCards() {
        for (int i = 0; i < BUTTONS; i++) selectedCard[i] = null;
    }
    */
}
