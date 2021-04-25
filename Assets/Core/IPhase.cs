
public interface IPhase {
    void OnClick(object source, InputData data);
    void OnClickUp(object source, InputData data);
    void OnEnterHold(object source, InputData data);
    void OnHold(object source, InputData data);
    void OnExitHold(object source, InputData data);
    void EnterPhase();
    void ExitPhase();
}

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseBehaviour : MonoBehaviour  {


    protected virtual void OnClick(object source, InputData data) { }
    protected virtual void OnClickUp(object source, InputData data) { }
    protected virtual void OnEnterHold(object source, InputData data) { }
    protected virtual void OnHold(object source, InputData data) { }
    protected virtual void OnExitHold(object source, InputData data) { }
    protected virtual void EnterPhase() { }
    protected virtual void ExitPhase() { }
}
*/