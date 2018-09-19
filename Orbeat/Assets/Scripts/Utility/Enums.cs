 
public enum GameState
{
    Start,
    Restart,
    Quit
}  

public enum SFX
{
    PlayerBlast
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
