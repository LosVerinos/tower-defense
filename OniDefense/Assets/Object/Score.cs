using System;
public class Score
{
	private string _playerName;
	private int _wave;

	public Score()
	{
		this._playerName = "default";
		this._wave = 1; 
	}

	public Score(string playerName)
	{
		this._playerName = playerName;
		this._wave = 1; 
	}

    public Score(string playerName, int wave)
    {
        this._playerName = playerName;
        this._wave = wave;
    }

    public void AddScore()
	{
		this._wave++;
	}
}

