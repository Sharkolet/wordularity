using System;
using System.Collections.Generic;

public class Game {
	private static readonly string[] PLAYER_NAMES = {"Donatello", "Raphael", "Michelangelo", "Leonardo"};

	private int currentPlayerIndex;
	private List<Player> players;
	private Round currentRound;

	public Game(int playersAmount) {
		currentPlayerIndex = 0;
		players = new List<Player>();
		for (int i = 0; i < playersAmount; ++i) {
			players.Add(new Player(PLAYER_NAMES[i]));
		}
	}

	public Round createRound() {
		return currentRound = new Round();
	}

	public List<Player> getPlayers() {
		return players;
	}

	public Player getCurrentPlayer() {
		return players[currentPlayerIndex];
	}

	public Round getCurrentRound() {
		return currentRound;
	}

    public Player nextPlayer()
    {
        currentPlayerIndex++;
        if (currentPlayerIndex == players.Count)
        {
            currentPlayerIndex = 0;
        }

        return players[currentPlayerIndex];
    }

    public int getCurrentPlayerIndex()
    {
        return currentPlayerIndex;
    }

    // Last player selected all rounds
    public bool isFinished()
    {
        //return currentPlayerIndex == players.Count - 1 && getCurrentPlayer().getPlayerRounds().Count == 1;
        return currentPlayerIndex == players.Count - 1 && getCurrentPlayer().getPlayerRounds().Count == Enum.GetValues(typeof(RoundWinType)).Length - 1;
    }
}
