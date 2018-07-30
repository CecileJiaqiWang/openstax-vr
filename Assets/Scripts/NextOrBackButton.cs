using UnityEngine;


public class NextOrBackButton : MonoBehaviour
{
    // Whether the Google Cardboard user is gazing at this button.
    private bool _isLookedAt = false;

    // How long the user can gaze at this before the button is clicked.
    public float TimerDuration = 1f;

    // Count time the player has been gazing at the button.
    private float _lookTimer = 0f;

    // Links with book logic
    public Book Book;

    // True means next, false means back
    public bool NextOrBack;

    public void SetGazedAt(bool gazedAt)
    {
        _isLookedAt = gazedAt;
    }


    void Update()
    {
        // While player is looking at this button.
        if (_isLookedAt)
        {
            // Increment the gaze timer.
            _lookTimer += Time.deltaTime;

            // Gaze time exceeded limit - button is considered clicked.
            if (_lookTimer > TimerDuration)
            {
                _lookTimer = 0f;
                if (NextOrBack == true)
                {
                    Book.Next();
                    Debug.Log("NExt");
                }
                else
                {
                    Book.Back();
                }
            }
        }
        else
        {
            _lookTimer = 0f;
        }
    }

    /*
     * Hides / displays the button according to the view mode.
     */
    public void Hide()
    {
       
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
      
    }

    void Start()
    {
       
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

    }
}