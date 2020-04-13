namespace Game.Additional
{
    /// <summary>
    /// Enums which defines difficults
    /// </summary>
    public enum EDifficult : byte
    { 
        Easy = 0,
        Medium,
        Hard
    }

    /// <summary>
    /// Enum which defines sizes of game field
    /// </summary>
    public enum ESize : byte
    { 
        _3x3 = 0,
        _4x4,
        _5x5,
        _6x6,
        _7x7
    }

    /// <summary>
    /// Enum which defines one of type games
    /// </summary>
    public enum ETypeGame : byte
    { 
        Classic = 0,
        Puzzle,
        PuzzleImage
    }

    /// <summary>
    /// Enum which defines frequency of save
    /// </summary>
    public enum EFrequencySave : byte
    { 
        EverySwap = 0,
        Every3Swaps,
        Every5Swaps,
        Every10Swaps,
        Every25Swaps,
        onlyByButtonSave
    }

    /// <summary>
    /// Status of ad
    /// </summary>
    public enum EAdStatus : byte
    { 
        Loads,
        Failed,
        Loaded,
        Shows,
        NoEthernet
    }

    /// <summary>
    /// Defines which types of ads provides, and what current ad type
    /// </summary>
    public enum ETypesAd : byte
    { 
        None = 0,
        Banner = 1 << 0,
        Image = 1 << 1,
        Video = 1 << 2,
        VideoGreetings = 1 << 3
    }
}
