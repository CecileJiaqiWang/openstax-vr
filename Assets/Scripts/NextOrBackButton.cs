using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NextOrBackButton : MonoBehaviour {

	// Whether the Google Cardboard user is gazing at this button.
	private bool isLookedAt = false;

	// How long the user can gaze at this before the button is clicked.
	public float timerDuration = 1f;

	// Count time the player has been gazing at the button.
	private float lookTimer = 0f;

	// Links with book logic
	public BookLogic bookLogic;

	// True means next, false means back
	public bool nextOrBack;

	// Use this for initialization
	void Start () {
//		myRenderer = GetComponent<Renderer>();
	}

	public void SetGazedAt(bool gazedAt) {
		isLookedAt = gazedAt;
	}

	void Update () {
		// While player is looking at this button.
		if (isLookedAt) {

			// Increment the gaze timer.
			lookTimer += Time.deltaTime;

//			gazeTimer.GetComponent<Renderer>().material.SetFloat("_Cutoff", lookTimer / timerDuration);

			// Gaze time exceeded limit - button is considered clicked.
			if (lookTimer > timerDuration) {
				lookTimer = 0f;

				if (nextOrBack == true) {
					bookLogic.Next();
				} else {
					bookLogic.Back();
				}
			}
		} else {
			lookTimer = 0f;
		}

	}
		
}