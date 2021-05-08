using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : CardEffect {
    public int amount;
    PlayerController player;

    public override ITarget[] RequestTargets() {
        return new ITarget[]{
            new SlotTarget(),
            new SlotTarget()
        };
    }

    public override void Apply(object[] args) {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        for (int i = 0; i < amount; i++) player.Draw();
    }
}
