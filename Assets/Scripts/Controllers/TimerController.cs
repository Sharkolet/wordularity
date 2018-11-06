using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour {
	private static readonly float ROUND_TIME = 120f;

	public GameController gameController;
	public Text txtTimer;
	private float timeLeft = ROUND_TIME;
	private bool activated;

	// Use this for initialization
	void Start() {
		reset();
	}
	
	// Update is called once per frame
	void Update() {
		if (activated) {
			timeLeft -= Time.deltaTime;

			if (timeLeft > 0) {
				updateLabel();
			} else {
				activated = false;
				gameController.onRoundFinished();
			}
		}
	}

	private string AdjustTimeValue(float value) {
		string result = value.ToString();
		if (value < 10) {
			result = "0" + result;
		}
		return result;
	}

	public void switchActiveState() {
		activated = !activated;
	}

	public void reset() {
		activated = false;
		timeLeft = ROUND_TIME;
		updateLabel();
	}

    public bool stopTimer()
    {
        bool isPossible = activated;
        if (isPossible)
        {
            activated = false;
            timeLeft = 0;
            updateLabel();
        }

        return isPossible;
    }

	private void updateLabel() {
		int minutes = (int)timeLeft / 60;
		int seconds = (int)Mathf.Floor (timeLeft % 60);
		txtTimer.text = AdjustTimeValue(minutes) + ":" + AdjustTimeValue(seconds);
	}
}
