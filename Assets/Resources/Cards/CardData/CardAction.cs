using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAction : MonoBehaviour {
    public string name;
    [SerializeField] CardEffect[] effects;
    ITarget[][] targets;


    void Awake() {
        targets = new ITarget[effects.Length][];
        for (int i = 0; i < effects.Length; i++) targets[i] = effects[i].RequestTargets();
    }

    /// <summary>
    /// Returns Targets needed to Apply this CardAction
    /// </summary>
    /// <returns></returns>
    public ITarget[] RequestTargets() {
        return Flat(targets);
    }

    /// <summary>
    /// Apply all CardEffect for the CardAction
    /// </summary>
    /// <param name="args"></param>
    public void Apply(object[] args) {
        int offset = 0;
        for (int i = 0; i < targets.Length; i++) {
            //if (targets[i] != null && targets[i].Length > 0) {
            Debug.Log(args.Length);
            Debug.Log(targets.Length);
            Debug.Log(targets[0]);
            Debug.Log(targets[0].Length);
            object[] effectArgs = SubArray(args, offset, targets[i].Length); //args; // Tomar sub array de args[i].Length elementos (calcular offset)
                effects[i].Apply(effectArgs);
                offset += targets[i].Length;
            //}
        }
    }

    /*
    public void foo() {
        System.Type.GetType(new CardTarget().type);
    }
    */

    T[] SubArray<T>(T[] array, int offset, int length) {
        T[] result = new T[length];
        System.Array.Copy(array, offset, result, 0, length);
        return result;
    }

    T[] Flat<T>(T[][] array) {
        List<T> result = new List<T>();
        for (int i = 0; i < array.Length; i++) {
            result.AddRange(array[i]);
        }
        return result.ToArray();
    }
}



