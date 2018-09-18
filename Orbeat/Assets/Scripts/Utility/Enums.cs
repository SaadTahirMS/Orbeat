 
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
	CharacterSelection
}

public enum WarningType
{
	NoVideoAvailable,
	InAppPurchaseFailed,
	NotEnoughCash
}
