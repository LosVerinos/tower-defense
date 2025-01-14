using System;
public class GameManager
{
    private int _waveNumber;
	private int _lives;
	private int _gold;
	private LeaderBoard _leaderBoard;

	private GameManager _gameManager;

    public GameManager()
	{
		if(this._gameManager == null)
		{
			initGameManager();
		}
	}

	public void initGameManager()
	{
		this._gameManager = new GameManager();
		this._leaderBoard = new LeaderBoard();
		this._lives = 100;
		this._gold = 100;
		
	}

	public void StartGame()
	{

	}

	public void EndGame()
	{

	}

	public void UpdateWave()
	{

	}

	public void AddGold(int amount)
	{
		//add verification
		this._gold += amount;
	}

	public void LoseLife()
	{

	}

}

