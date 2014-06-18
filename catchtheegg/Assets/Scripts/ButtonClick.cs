using UnityEngine;
using System.Collections;

public enum ButtonType
{
	ClickHere,
	Continue
};

public class ButtonClick : MonoBehaviour {


	public ButtonType btnType;
	private GameController gameController;

	public GameObject GameControllerLayer;

	void Start() {

		if (GameControllerLayer != null)
		{
			gameController = GameControllerLayer.GetComponent <GameController>();
		}
	}

	void OnMouseDown() {
		switch(btnType) {
		    case ButtonType.ClickHere:
			    gameController.clicked_ClickHereButton();
				break;
		    case ButtonType.Continue:
			    gameController.clicked_ElephantButton();
				break;
			default:
				//Do Nothing
				break;
		}
	}
}
