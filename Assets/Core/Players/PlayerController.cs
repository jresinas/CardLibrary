using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Setup {
    public Slot slot;
    public CardData[] cards;
}
public class PlayerController : MonoBehaviour {
    public int player;
    public Setup[] setups;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
