using UnityEngine;
using System.Collections;
public class scaleFont : MonoBehaviour {
	
	
	
	public Vector2 offset;
	
	
	
	public float ratio = 20;
	
	
	
	// Use this for initialization
	
	void Start () {
		
		
		
	}
	
	
	
	// Update is called once per frame
	
	void Update () {
		
		
		
	}
	
	void OnGUI(){
		
		
		
		float finalSize = (float)Screen.width/ratio;
		
		guiText.fontSize = (int)finalSize;
		
		guiText.pixelOffset = new Vector2( offset.x * Screen.width, offset.y * Screen.height);
		
	}
	
	
	
	
	
}