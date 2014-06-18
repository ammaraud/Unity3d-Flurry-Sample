using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;
	
	// How long the object should shake for.
	private float shake = 0.1f;
	public float initailShake = 0.1f;
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.4f;
	public float decreaseFactor = 1.0f;
	
	Vector3 originalPos;
	
	public void Awake()
	{
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}
	
	void OnEnable()
	{
		originalPos = camTransform.localPosition;
		shake = initailShake;
	}
	
	void Update()
	{
		if (shake > 0)
		{
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			
			shake -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			//shake = 0.1f;
			camTransform.localPosition = originalPos;
			enabled = false;
		}
	}
}