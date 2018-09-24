 
public enum GameState
{
    Initial,
    Start,
    Revive,
    Quit
}  

public enum SFX
{
    PlayerBlast,
	ButtonClick,
	LevelUp,
	GameOver,
	HighScore,
	Ready,
	Go
}

public enum Views : int
{
	MainMenu = 0,
	Settings,
	ConfirmationDialog,
	Pause,
	RateUs,
	CharacterSelection,
	GameOver
}

public enum WarningType
{
	NoVideoAvailable,
	InAppPurchaseFailed,
	NotEnoughCash
}

public enum ModeType
{
    NormalMode = 0,
    ClockWise = 1,
    AntiClockWise = 2,
    PingPongMode = 3,
}
