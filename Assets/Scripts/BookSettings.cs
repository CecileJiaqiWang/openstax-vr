﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Buttons{
    towardPlayer, awayPlayer, rotateTopBook, rotateBotBook,
	toggleHide
 }

public class BookSettings : MonoBehaviour {

	
	// Gets the book
	public GameObject BookInterface;
	public GameObject book;

	private Boolean _isHiden = false;
	
	// The Gameobjects to be activated.
	private bool[,] recover = new bool[7, 21];

	// Button choices
	public Buttons current;

	// How long the user can gaze at this before the button is clicked.
	public float timerDuration = 1f;

	// Count time the player has been gazing at the button.
	private float lookTimer = 0f;

	// Whether the Google Cardboard user is gazing at this button.
	private bool isLookedAt = false;

	public void SetGazedAt(bool gazedAt) {
		isLookedAt = gazedAt;
	}

	private void ToggleHide()
	{
		_isHiden = !_isHiden;
		Transform parent = BookInterface.gameObject.transform;
		if (_isHiden)
		{
			// Hide or deactivate game objects.
			for (int i = 0; i < parent.childCount; i++)
			{
				Transform child = parent.GetChild(i);
				for (int j = 0; j < child.childCount; j++)
				{
					Transform grandChild = child.GetChild(j);
					grandChild.gameObject.SetActive(false);
				}								
			}

		}
		else
		{
			// Recover game objects.
			parent.GetChild(1).GetChild(5).transform.localScale = new Vector3(1, 1, 1);
			for (int i = 0; i < parent.childCount; i++)
			{
				for (int j = 0; j < parent.GetChild(i).childCount; j++)
				{
					if (recover[i, j] == true)
					{
						parent.GetChild(i).GetChild(j).gameObject.SetActive(true);
						recover[i, j] = false;
					}
				}
			}
		}
		
	}
	
	// Update is called once per frame
	void Update () {

		// Player looking at the buttons
		if(isLookedAt){

			// Increment the gaze timer.
			lookTimer += Time.deltaTime;

			// Gaze time exceeded limit - button is considered clicked.
			if (lookTimer > timerDuration) {
				lookTimer = 0f;

				switch (current){
					case Buttons.towardPlayer: // Moves the book toward the user
						book.transform.position += new Vector3(-1,0,0);
						break;
					case Buttons.awayPlayer: // Moves the book away from the user
						book.transform.position += new Vector3(1,0,0);
						break;
					case Buttons.rotateTopBook: // Rotates the top of the book toward the user
						book.transform.Rotate(-10,0,0);
						break;
					case Buttons.rotateBotBook: // Rotates the bottom of the book toward the user
						book.transform.Rotate(10,0,0);
						break;
					case Buttons.toggleHide:
						ToggleHide();
						break;
						
				}
			}
		} else {
			lookTimer = 0f;
		}
	}
}
