using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

[System.Serializable]
public class CardField {
    public string fieldName;
    public TextMeshProUGUI field;
}

public class CardController : Card {
    float ZOOM_DISTANCE = 1.5f;
    [SerializeField] TransformAnimator anim;
    [SerializeField] Vector3 currentPosition;
    [SerializeField] Vector3 currentRotation;
    [SerializeField] CardField[] cardFields;
    Dictionary<string, TextMeshProUGUI> cardFieldsDict = new Dictionary<string, TextMeshProUGUI>();
    bool revealed = false;

    void Awake() {
        foreach (CardField cardField in cardFields) {
            cardFieldsDict[cardField.fieldName] = cardField.field;
        }
    }

    void Start() {
        if (data != null) {
            LoadEffects();
            LoadTexts();
        } else Debug.LogError("Card data could not be found");
    }

    void LoadTexts() {
        foreach (CardText cardText in data.cardTexts) {
            if (cardFieldsDict.ContainsKey(cardText.fieldName)) cardFieldsDict[cardText.fieldName].text = cardText.text;
        }
    }

    void LoadEffects() {
        MonoScript script = data.effects[0];
        gameObject.AddComponent(script.GetClass());
    }

    // Update card position
    public void UpdatePosition(Vector3 newPosition, bool animation = true) {
        if (animation) anim.StartTransformAnimation(newPosition, currentRotation, 0.2f);
        else transform.position = newPosition;
        currentPosition = newPosition;
    }

    // Update card rotation
    public void UpdateRotation(Vector3 newRotation, bool animation = true) {
        if (animation) anim.StartTransformAnimation(currentPosition + Vector3.up, newRotation, 0.5f);
        else transform.eulerAngles = newRotation;
        currentRotation = newRotation;
    }

    // Flip the card
    public void Flip(int player) {
        //if (base.Flip(player)) {
        if (AllowFlip(player) && GetSlot() != null && GetSlot().AllowFlip(player, this)) {
            revealed = !revealed;
            UpdateRotation(currentRotation + CardFlip());
            StartCoroutine(ExecuteAfterTime(0.4f));
        }

        IEnumerator ExecuteAfterTime(float time) {
            yield return new WaitForSeconds(time);
            anim.StartTransformAnimation(currentPosition, currentRotation, 0.3f);
        }
    }

    // Card animation when enter zoom revealing card
    public void EnterZoomReveal(int player) {
        anim.StartTransformAnimation(CameraMiddle(), currentRotation + CameraParallel() + CardFlip(), 0.35f);
    }

    // Card animation when enter zoom
    public void EnterZoom() {
        anim.StartTransformAnimation(CameraMiddle(), currentRotation + CameraParallel(), 0.3f);
    }

    // Card animation when stop zoom
    public void ExitZoom() {
        Debug.Log(this.name);
        anim.StartTransformAnimation(currentPosition, currentRotation, 0.3f);
    }

    // Card animation when stop dragging
    public void ExitDrag() {
        anim.StartTransformAnimation(currentPosition, currentRotation, 0.15f);
    }

    // Return rotation to flip de card
    Vector3 CardFlip() {
        return new Vector3(0f, 0f, 180f);
    }

    // Return rotation to show card on camera
    Vector3 CameraParallel() {
        return new Vector3(Camera.main.transform.eulerAngles.x - 90f, 0f, 0);
    }

    // Return middle of the screen
    Vector3 CameraMiddle() {
        return Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, ZOOM_DISTANCE));
    }

    /*
    public void Reveal(bool state) {
        revealed = state;
        if (revealed) UpdateRotation
    }
    */
}
