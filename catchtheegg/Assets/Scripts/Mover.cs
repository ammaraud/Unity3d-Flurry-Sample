using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public float speed;
	public bool stopMoving = false;

	void Start() {
//		Debug.Log("Screen.width: " + Screen.width);
	}

	void FixedUpdate() {

		if (!stopMoving) {
			transform.position = new Vector3 (transform.position.x - speed, transform.position.y, transform.position.z);
		}

//		Debug.Log("Destroying Obejct: " + transform.position.x);
		if (transform.position.x < (-9.5)) {
//			Debug.Log("Destroying Obejct");
			Destroy(gameObject);
		}
	}

	public void stopMovingObstacles() {
		stopMoving = true;
	}
}
