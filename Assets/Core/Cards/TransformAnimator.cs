using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformAnimator : MonoBehaviour {
    Vector3 originPosition;
    Vector3 targetPosition;
    Vector3 originRotation;
    Vector3 targetRotation;
    float animationTime;

    bool animating = false;
    float time = 0;


    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (animating) Step(Time.deltaTime);
    }

    public void StartTransformAnimation(Vector3 targetPosition, Vector3 targetRotation, float animationTime) {
        this.originPosition = transform.position;
        this.targetPosition = targetPosition;
        this.originRotation = transform.eulerAngles;
        this.targetRotation = targetRotation;
        this.animationTime = animationTime;

        animating = true;
        time = 0;
    }

    void Step(float t) {
        time += t;

        if (time/animationTime >= 1) {
            time = animationTime;
            animating = false;
        }

        transform.position = Vector3.Lerp(originPosition, targetPosition, time/animationTime);
        //transform.eulerAngles = Vector3.Lerp(originRotation, targetRotation, time/animationTime);
        transform.eulerAngles = new Vector3(Mathf.LerpAngle(originRotation.x, targetRotation.x, time/animationTime),
            Mathf.LerpAngle(originRotation.y, targetRotation.y, time / animationTime),
            Mathf.LerpAngle(originRotation.z, targetRotation.z, time / animationTime));
    }
}
