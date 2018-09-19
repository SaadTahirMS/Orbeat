 
public enum GameState
{
    Start,
    Restart,
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
