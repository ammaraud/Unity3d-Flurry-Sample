using UnityEngine;
using System.Collections;

public enum Medal {
	BRONZE,
	SILVER,
	GOLD
}

public class GameOverScript : MonoBehaviour {

	public GUIText score;
	public GUIText bestScore;

	public GameObject bronzeMedal;
	public GameObject silverMedal;
	public GameObject goldMedal;

	void Start() {
		hideMedals ();
	}

	private void hideMedals() {
		bronzeMedal.SetActive (false);
		silverMedal.SetActive (false);
		goldMedal.SetActive (false);

	}

	public void setScore(int _score) {
		score.text = "" + _score;
	}

	public void setBestScore (int _bestScore) {
		bestScore.text = "" + _bestScore;
	}

	public void showMedal(int _score) {
		hideMedals ();
		if (_score < 10) {
			bronzeMedal.SetActive(true);
		} else if (_score < 25) {
			silverMedal.SetActive(true);
		} else {
			goldMedal.SetActive(true);
		}
	}

}
