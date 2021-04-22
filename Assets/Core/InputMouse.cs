using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputData {
    public int button;
    public CardController selectedCard;
    public Slot targetSlot;
    public CardController targetCard;

    public InputData(int button, CardController selectedCard, Slot targetSlot, CardController targetCard) {
        this.button = button;
        this.selectedCard = selectedCard;
        this.targetSlot = targetSlot;
        this.targetCard = targetCard;
    }
}

public class InputMouse : MonoBehaviour {
    protected float PRESS_THRESHOLD = 0.15f;

    [SerializeField] protected int state = 0;
    [SerializeField] protected bool[] hold = new bool[GameManager.BUTTONS];
    [SerializeField] protected float[] press = new float[GameManager.BUTTONS];
    [SerializeField] protected bool[] drag = new bool[GameManager.BUTTONS];
    [SerializeField] protected CardController[] selectedCard = new CardController[GameManager.BUTTONS];

    public virtual event EventHandler<InputData> OnClick;
    public virtual event EventHandler<InputData> OnClickUp;
    public virtual event EventHandler<InputData> OnEnterHold;
    public virtual event EventHandler<InputData> OnHold;
    public virtual event EventHandler<InputData> OnExitHold;

    protected virtual void ChangeState(int current, int next) { }
    
    void Update() {
        for (int button = 0; button < GameManager.BUTTONS; button++) ButtonBehaviour(button);
    }

    void ButtonBehaviour(int button) {
        Slot targetSlot = GetTarget<Slot>();
        CardController targetCard = GetTarget<CardController>();

        if (Input.GetButtonDown("Button" + button)) {
            selectedCard[button] = GetCard();
            if (OnClick != null) OnClick(this, new InputData(button, selectedCard[button], targetSlot, targetCard));
        }

        if (Input.GetButtonUp("Button" + button)) {
            CardController card = GetCard();
            if (hold[button]) {
                hold[button] = false;
                if (OnExitHold != null) OnExitHold(this, new InputData(button, selectedCard[button], targetSlot, targetCard));
            } else if (card != null && card == selectedCard[button]) {
                if (OnClickUp != null) OnClickUp(this, new InputData(button, selectedCard[button], targetSlot, targetCard));
            }
        }

        if (Input.GetButton("Button" + button)) {
            CardController card = GetCard();
            if (card != null && card == selectedCard[button]) press[button] += Time.deltaTime;
            else press[button] = 0;
        } else {
            press[button] = 0;
        }

        if (press[button] > PRESS_THRESHOLD && !hold[button]) {
            if (OnEnterHold != null) OnEnterHold(this, new InputData(button, selectedCard[button], targetSlot, targetCard));
            hold[button] = true;
        }

        if (hold[button]) {
            if (OnHold != null) OnHold(this, new InputData(button, selectedCard[button], targetSlot, targetCard));
        }
    }

    /// <summary>
    /// Returns the card below mouse pointer
    /// </summary>
    /// <returns></returns>
    protected CardController GetCard() {
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
    protected T GetTarget<T>() {
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

}
