
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

public enum BubbleState {
    Moving,
    MovingToBase,
    FightingUnit,
    FightingBase
}

public enum AudioClips {
    BGM,
    Pop,
    Spawn,
    ButtonClicked,
    WarriorHit,
    ArcherHit,
    FloatyHit
}

public enum SceneNames {
    MainMenu,
    Game
}

public enum ColliderTags {
    Lane,
    BubbleUnit,
    BubbleBase,
}

public enum AnimatorTriggers {
    StartMove,
    StartFight,
}