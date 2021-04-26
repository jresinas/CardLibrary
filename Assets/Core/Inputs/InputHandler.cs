using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEventData {
    public int button;
    public CardController selectedCard;
    public Slot targetSlot;
    public CardController targetCard;

    public InputEventData(int button, CardController selectedCard, Slot targetSlot, CardController targetCard) {
        this.button = button;
        this.selectedCard = selectedCard;
        this.targetSlot = targetSlot;
        this.targetCard = targetCard;
    }
}
public class InputHandler : MonoBehaviour {
    [SerializeField] InputMouse input;
    [SerializeField] InputManager manager;

    void Awake() {
        if (input != null) {
            //input.OnClick += ((InputCustom)this).OnClick;
            //input.OnClickUp += ((InputCustom)this).OnClickUp;
            //input.OnEnterHold += ((InputCustom)this).OnEnterHold;
            //input.OnHold += ((InputCustom)this).OnHold;
            //input.OnExitHold += ((InputCustom)this).OnExitHold;
            input.OnClick += manager.OnClick;
            input.OnClickUp += manager.OnClickUp;
            input.OnEnterHold += manager.OnEnterHold;
            input.OnHold += manager.OnHold;
            input.OnExitHold += manager.OnExitHold;
        } else Debug.LogError("Input or Input Handler not found");
    }

    

    //protected abstract void OnClick(object source, InputEventData data);
    //protected abstract void OnClickUp(object source, InputEventData data);
    //protected abstract void OnEnterHold(object source, InputEventData data);
    //protected abstract void OnHold(object source, InputEventData data);
    //protected abstract void OnExitHold(object source, InputEventData data);
}
