using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCustom : InputMouse {
    protected override void ActionClick(int button) {
        switch (button) {
            case 0:
                Flip(button);
                //Custom1(button);
                break;
            case 1:
                EnterZoom(button);
                break;
        }
    }

    protected override void EnterActionHold(int button) {
        switch (button) {
            case 0:
                EnterDrag(button);
                break;
            case 1:
                EnterZoomReveal(button);
                break;
        }
    }

    protected override void ExitActionHold(int button) {
        switch (button) {
            case 0:
                ExitDrag(button);
                break;
            case 1:
                // Never is called because EnterZoom and EnterZoomReveal change InputMouse state
                break;
        }
    }

    /* Example */
    public Slot customSlotOrigin;
    public Slot customSlotDestiny;
    void Custom1(int button) {
        if (selectedCard[button] != null) {
            Slot currentSlot = selectedCard[button].GetSlot();

            if (currentSlot == customSlotOrigin) selectedCard[button].Move(player, customSlotDestiny);
            else Flip(button);
        }
    }

}
