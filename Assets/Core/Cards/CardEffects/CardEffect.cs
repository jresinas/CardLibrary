using UnityEngine;

public abstract class CardEffect : MonoBehaviour {
    /// <summary>
    /// Returns Targets needed to Apply this CardEffect
    /// </summary>
    /// <returns></returns>
    public abstract ITarget[] RequestTargets();

    /// <summary>
    /// Apply the CardEffect
    /// </summary>
    /// <param name="args"></param>
    public abstract void Apply(object[] args);
}
