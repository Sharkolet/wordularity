using System.Collections.Generic;

public class Player {
	private string playerName;
    private List<Round> playerRounds;
    public int bonusPoints { get; set; }

	public Player(string name)
    {
		this.playerName = name;
        this.playerRounds = new List<Round>(8);
	}

	public string getPlayerName()
    {
		return playerName;
	}

	public void addRound(Round round) {
		playerRounds.Add(round);
	}

    public List<Round> getPlayerRounds()
    {
        return playerRounds;
    }
}
