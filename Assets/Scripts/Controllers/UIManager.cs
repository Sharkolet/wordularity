using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    private readonly Color defaultButtonColor = Color.white;
    private readonly Color wrongButtonColor = Color.red;

	public GameObject enterBtn;
	public GameObject clearBtn;
	public GameObject pauseBtn;
	public GameObject finishBtn;
	public GameObject exitBtn;

	public GameObject prepareScreen;
	public GameObject winScreen;
	public GameObject preparePlayerName;
	public GameObject winPlayerName;
	public GameObject pauseScreen;
	public GameObject pauseDarkPanel;

	public GameObject wordPrefab;
	public GameObject wordListContent;
	public GameObject wordsAmount;

	public GameObject[] playerIcons;
    public GameObject[] randomLetters;
    public GameObject[] wordLetters;
    public GameObject[] playersScore;

	public GameObject roundFinishText;
    public ParticleSystem fireworks1;
    public ParticleSystem fireworks2;

	public void resetUI()
    {
		foreach (GameObject obj in randomLetters) {
			obj.SetActive(false);
		}
		foreach (GameObject obj in wordLetters) {
			obj.SetActive(false);
		}
		enterBtn.GetComponent<Button>().interactable = false;
		clearBtn.GetComponent<Button>().interactable = false;
		prepareScreen.SetActive(false);
		pauseScreen.SetActive(false);
		pauseDarkPanel.SetActive(false);
        roundFinishText.SetActive(false);
    }

	public void updatePlayerIcons()
    {
		foreach (GameObject obj in playerIcons) {
			obj.SetActive(false);
		}
		for (int i = 0; i < GameState.playersAmount; ++i) {
			playerIcons[i].SetActive(true);
		}
	}

	public void showPrepareScreen(string name)
    {
		preparePlayerName.GetComponent<Text>().text = name;
		prepareScreen.SetActive(true);
		pauseDarkPanel.SetActive(true);
	}

    public void showWinScreen(string name)
    {
        Sounds.AMBIENT.Stop();
        Sounds.GORN.Play();
        Sounds.CROWD.Play();
        Sounds.FIREWORKS.Play();
        fireworks1.Play();
        fireworks2.Play();
        winPlayerName.GetComponent<Text>().text = name + " wins.";
        winScreen.SetActive(true);
        pauseDarkPanel.SetActive(true);
    }

    public void showPauseScreen()
    {
		pauseScreen.SetActive(true);
		pauseDarkPanel.SetActive(true);
	}

	public void showRoundFinished()
    {
		pauseBtn.GetComponent<Button>().interactable = false;
        finishBtn.GetComponent<Button>().interactable = false;
		roundFinishText.SetActive(true);
	}

	public void resumeUI()
    {
		prepareScreen.SetActive(false);
		pauseScreen.SetActive(false);
		pauseDarkPanel.SetActive(false);
	}

	public void updateRoundUI(Round round)
    {
		// Resetting random letters
		for (int i = 0; i < randomLetters.Length; ++i) {
			randomLetters[i].GetComponentInChildren<Text>().text = round.getLetters()[i].ToString();
			randomLetters[i].SetActive(true);
		}

		// Words list update
		wordListContent.transform.DetachChildren();
		foreach (string word in round.getWords()) {
			GameObject wordText = Instantiate(wordPrefab, wordListContent.transform, false);
			wordText.GetComponent<Text>().text = word;
		}
		Canvas.ForceUpdateCanvases();
		GameObject.Find("Scroll View").GetComponent<ScrollRect>().verticalNormalizedPosition = 0;
		Canvas.ForceUpdateCanvases();
		wordsAmount.GetComponent<Text>().text = "" + round.getWords().Count;
	}

	public void addLetter(GameObject letterBtn)
    {
		foreach (GameObject btn in wordLetters) {
			if (btn.activeSelf) {
				continue;
			}
			btn.GetComponentInChildren<Text>().text = letterBtn.GetComponentInChildren<Text>().text;
			btn.SetActive(true);
			break;
		}
		letterBtn.SetActive(false);
		clearBtn.GetComponent<Button>().interactable = true;
		enterBtn.GetComponent<Button>().interactable = true;
	}

	public void clearOnEnter()
    {
		foreach (GameObject btn in randomLetters) {
			btn.SetActive(true);
		}
		foreach (GameObject btn in wordLetters) {
			btn.SetActive(false);
		}
		clearBtn.GetComponent<Button>().interactable = false;
		enterBtn.GetComponent<Button>().interactable = false;
	}

    public void clearForPrepareScreen()
    {
        roundFinishText.SetActive(false);
        wordListContent.transform.DetachChildren();
        wordsAmount.GetComponent<Text>().text = "0";
        pauseBtn.GetComponent<Button>().interactable = true;
        finishBtn.GetComponent<Button>().interactable = true;
    }

    public void updateCurrentPlayerScore(int playerIndex, int totalScore)
    {
        playersScore[playerIndex].GetComponent<Text>().text = totalScore.ToString();
    }

    public string grabCurrentWord()
    {
		string result = "";
		foreach (GameObject btn in wordLetters) {
			if (btn.activeSelf) {
				result += btn.GetComponentInChildren<Text>().text;
			} else {
				break;
			}
		}
		return result;
	}

    public void animateWrongEnter()
    {
        Sounds.BTN_INCORRECT.Play();
        Button btn = enterBtn.GetComponent<Button>();
        
        enterBtn.GetComponent<Image>().color = wrongButtonColor;
        btn.interactable = false;
        Invoke("enableEnter", 0.1f);
    }

    private void enableEnter()
    {
        Button btn = enterBtn.GetComponent<Button>();

        enterBtn.GetComponent<Image>().color = defaultButtonColor;
        btn.interactable = true;
    }
}
