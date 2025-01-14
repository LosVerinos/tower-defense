using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public class LeaderBoard
{
    public List<Score> scores = new List<Score>();

    private LeaderBoard leaderBoard;

    public LeaderBoard()
	{
        if (leaderBoard == null)
        {
            this.leaderBoard = new LeaderBoard();
        }
    }

    public LeaderBoard GetLeaderBoard()
    {
        return this.leaderBoard;
    }

    public void addScore(string playerName, int wave)
    {
        this.scores.Add(new Score(playerName, wave));
    }
}

