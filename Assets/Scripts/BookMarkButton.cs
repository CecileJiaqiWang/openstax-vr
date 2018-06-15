using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor.SceneManagement;

public class BookMarkButton : MonoBehaviour {

    // Whether the Google Cardboard user is gazing at this button.
    private bool isLookedAt = false;

    // How long the user can gaze at this before the button is clicked.
    public float timerDuration = 1f;

    // Count time the player has been gazing at the button.
    private float lookTimer = 0f;

    // Links with book logic
    public BookLogic Book;

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
            if (lookTimer > timerDuration)
            {
                lookTimer = 0;
                Book.SaveProgress();
                Debug.Log("Saved!");
            }
        } 

    }
    
    public void Hide()
    {
        if (Book.HideBook)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
            
        }
    }
    
    void Start()
    {
        if (Book.HideBook)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
				
            }
        }
    }
		
}