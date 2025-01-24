
public enum BubbleType {
    Warrior,
    Archer,
    Floaty,
    Sunflower,
    NONE
}

public enum LanePosition {
    Top,
    Middle,
    Bottom
}

public static class BubbleUtility {
    public static string BubbleTypeToString(BubbleType bubbleType) {
        return bubbleType.ToString();
    }
}