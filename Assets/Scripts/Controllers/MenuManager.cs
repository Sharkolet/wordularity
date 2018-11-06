using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
	public GameObject welcomeElements;
	public GameObject chooseModeElements;
	public GameObject choosePlayersAmountElements;

	public void showWelcome() {
		welcomeElements.SetActive(true);
		chooseModeElements.SetActive(false);
		choosePlayersAmountElements.SetActive(false);
	}

	public void showChooseMode() {
        Sounds.BTN_GENERAL.Play();
        welcomeElements.SetActive(false);
		chooseModeElements.SetActive(true);
		choosePlayersAmountElements.SetActive(false);
	}

	public void showChoosePlayersAmount() {
        Sounds.BTN_GENERAL.Play();
        welcomeElements.SetActive(false);
		chooseModeElements.SetActive(false);
		choosePlayersAmountElements.SetActive(true);
	}

	public void choosePlayersAmount(int playersAmount) {
        Sounds.BTN_GENERAL.Play();
        GameState.playersAmount = playersAmount;
		SceneManager.LoadScene("MainScene");
	}

    public void quit()
    {
        Application.Quit();
    }
}
