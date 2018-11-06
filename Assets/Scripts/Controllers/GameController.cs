using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	private Game currentGame;
	private Vocabulary vocabulary;
	public UIManager uiManager;
	public TimerController timer;
    public NotepadController notepadController;
    public LettersCalculator lettersCalculator;

    public GameObject[] selectablePoints;
    private Dictionary<GameObject, RoundWinType> pointsMap;

    void Start() {
		currentGame = new Game(GameState.playersAmount);
		vocabulary = new Vocabulary();
        initPoints();
        uiManager.updatePlayerIcons();
		uiManager.resetUI();
		uiManager.showPrepareScreen(currentGame.getCurrentPlayer().getPlayerName());
	}

    public void initPoints()
    {
        pointsMap = new Dictionary<GameObject, RoundWinType>();
        for (int i = 0; i < selectablePoints.Count(); ++i)
        {
            pointsMap.Add(selectablePoints[i], (RoundWinType)i);
        }
        enablePointsSelection(false);
    }

    public void exit() {
        Sounds.GORN.Stop();
        Sounds.FIREWORKS.Stop();
        Sounds.CROWD.Stop();
        if (!Sounds.AMBIENT.isPlaying)
        {
            Sounds.AMBIENT.Play();
        }
        Sounds.BTN_GENERAL.Play();
		SceneManager.LoadScene("MenuScene");
	}

	public void startNewRound() {
        Sounds.DICES_THROW.Play();
		Round round = currentGame.createRound();
		uiManager.updateRoundUI(round);
        notepadController.update(round, currentGame.getCurrentPlayer());
		resumeGame();
	}

	public void resumeGame() {
        Sounds.AMBIENT.UnPause();
        timer.switchActiveState();
		uiManager.resumeUI();
	}

	public void pauseGame() {
        Sounds.BTN_GENERAL.Play();
        Sounds.AMBIENT.Pause();
        timer.switchActiveState();
		uiManager.showPauseScreen();
	}

	public void addLetter(GameObject obj)
    {
        Sounds.LETTER_PICK.Play();
		uiManager.addLetter(obj);
	}

	public void enter() {
		Round currentRound = currentGame.getCurrentRound();
		string word = uiManager.grabCurrentWord();
		if (word.Length > 2 && vocabulary.containsWord(word) && !currentRound.getWords().Contains(word)) {
            Sounds.BTN_CORRECT.Play();
			currentRound.addWord(word);
			uiManager.updateRoundUI(currentRound);
            notepadController.update(currentRound, currentGame.getCurrentPlayer());
            lettersCalculator.updateUI(currentRound.getWords());
            uiManager.clearOnEnter();
        } else {
            uiManager.animateWrongEnter();
		}
	}

    public void clockPressed()
    {
        bool isPossible = timer.stopTimer();
        if (isPossible)
        {
            onRoundFinished();
        }
    }

	public void onRoundFinished()
    {
        Sounds.FINISH_ROUND.Play();
        uiManager.resetUI();
		uiManager.showRoundFinished();
        enablePointsSelection(true);
	}

    public void onPointsSelected(GameObject text)
    {
        Sounds.PENCIL.Play();
        enablePointsSelection(false);
        currentGame.getCurrentRound().winType = pointsMap[text];
        currentGame.getCurrentPlayer().addRound(currentGame.getCurrentRound());
        int totalPoints = notepadController.update(currentGame.getCurrentRound(), currentGame.getCurrentPlayer());
        currentGame.getCurrentPlayer().bonusPoints = notepadController.getBonusPoints(currentGame.getCurrentRound());
        uiManager.updateCurrentPlayerScore(currentGame.getCurrentPlayerIndex(), totalPoints);
        if (currentGame.isFinished())
        {
            Invoke("finishGame", 2);
        } else
        {
            Invoke("prepareNextPlayer", 2);
        }
    }

    void prepareNextPlayer()
    {
        timer.reset();
        Player nextPlayer = currentGame.nextPlayer();
        notepadController.reset();
        lettersCalculator.reset();
        uiManager.clearForPrepareScreen();
        uiManager.showPrepareScreen(nextPlayer.getPlayerName());
    }

    void finishGame()
    {
        timer.reset();
        notepadController.reset();
        lettersCalculator.reset();
        uiManager.clearForPrepareScreen();
        uiManager.showWinScreen(getPlayerWonName());
    }

    void clear()
    {
        Sounds.BTN_CLEAR.Play();
        uiManager.clearOnEnter();
    }

    private string getPlayerWonName()
    {
        string wonPlayer = currentGame.getPlayers()[0].getPlayerName();
        int maxScore = notepadController.getTotalWin(currentGame.getPlayers()[0]);
        foreach (Player p in currentGame.getPlayers())
        {
            int playerScore = notepadController.getTotalWin(p);
            if (playerScore >= maxScore)
            {
                wonPlayer = p.getPlayerName();
                maxScore = playerScore;
            }
        }

        return wonPlayer;
    }

    private void enablePointsSelection(bool enable)
    {
        foreach (GameObject points in pointsMap.Keys)
        {
            bool isAlreadyWon = currentGame.getCurrentPlayer().getPlayerRounds().Any(round => round.winType == pointsMap[points]);
            if (enable && isAlreadyWon)
            {
                continue;
            }
            points.GetComponent<EventTrigger>().enabled = enable;
        }
    }
}