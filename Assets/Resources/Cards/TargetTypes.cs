public class CardTarget : ITarget {
    public string type => "Card";

    public bool Validate(object obj) {
        return obj is Card;
    }
}

public class SlotTarget : ITarget {
    public string type => "Slot";

    public bool Validate(object obj) {
        return obj is Slot;
    }
}
