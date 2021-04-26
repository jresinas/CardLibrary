using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    public Deck playerDeck;
    public Board playerHand;
    [SerializeField] PlayerController player;

    public void OnClick(object source, InputEventData data) {
        /*
        if (base.IsZoom()) {
            Debug.Log("OnClick");
            base.ExitZoom();
        }
        */
    }

    public void OnClickUp(object source, InputEventData data) {
        if (!InputHelper.IsZoom()) {
            switch (data.button) {
                case 0:
                    //Flip(button);
                    //Custom2(button);
                    InputHelper.Draw(player, data.targetSlot, playerDeck, playerHand);
                    break;
                case 1:
                    //input.EnterZoom(data.button);
                    InputHelper.EnterZoom(data.selectedCard);
                    break;
            }
        } else InputHelper.ExitZoom();
    }

    public void OnEnterHold(object source, InputEventData data) {
        if (!InputHelper.IsZoom()) {
            switch (data.button) {
                case 0:
                    Debug.Log("OnEnterHold0");
                    //input.EnterDrag(data.button);
                    break;
                case 1:
                    Debug.Log("OnEnterHold1");
                    //input.EnterZoomReveal(data.button);
                    InputHelper.EnterZoomReveal(player, data.selectedCard);
                    break;
            }
        }
    }

    public void OnHold(object source, InputEventData data) {
        if (!InputHelper.IsZoom()) {
            switch (data.button) {
                case 0:
                    //input.Drag(data.button);
                    InputHelper.Drag(data.selectedCard);
                    break;
            }
        }
    }

    public void OnExitHold(object source, InputEventData data) {
        if (!InputHelper.IsZoom()) {
            switch (data.button) {
                case 0:
                    InputHelper.Drop(player, data.selectedCard, data.targetSlot);
                    break;
                case 1:
                    // Never is called because EnterZoom and EnterZoomReveal change InputMouse state
                    break;
            }
        } else InputHelper.ExitZoom();
    }
}
