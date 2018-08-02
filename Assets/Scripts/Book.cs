// Creates a system for viewing a set of images all grouped under one parent gameObject
// Create two button (forward, backward) and attached them to the parent gameObject 

using System;
using System.IO;
using UnityEngine;


public class Book : MonoBehaviour
{
    public GameObject Parent;
    public GameObject BookInterface;

    private string _bookChoices;
    private int _currentPage = 0;
    private int _progress;
    private string _url = "http://cdf7.web.rice.edu/OpenStaxVR_iOS/";
    private AssetBundle _previousBundle;
    private AssetBundle _currentBundle;
    private AssetBundle _nextBundle;
    [NonSerialized]
    public Boolean IsTable = false;

    /**
     * Gets the total number of pages.
     */
    private int TotalPageCount()
    {
        int totalPage = 4;
        return totalPage;
    }
    
    /**
     * Update current page to one forward.
     */
    private int NextPage(int currentPage)
    {
        return currentPage == TotalPageCount() - 1 
            ? 0 
            : currentPage + 1;
    }

    /**
     * Update current page to one backward.
     */
    private int PreviousPage(int currentPage)
    {
        return currentPage == 0
            ? TotalPageCount() - 1
            : currentPage - 1;
    }

    /**
     * Displays the next page.
     */
    public void TurnNextPage()
    {
        _currentPage = NextPage(_currentPage);
        
        // Save progress.
        SaveProgress();
        // Update active page.
        UpdatePages();
    }

    /**
     * Displays the previous page.
     */
    public void TurnBackPage()
    {
        _currentPage = PreviousPage(_currentPage);
        
        // Save progress.
        SaveProgress();

        // Update active page.
        UpdatePages();
    }

    public void GoToPage(int pageNum)
    {
        _currentPage = pageNum;
        
        // Save progress.
        SaveProgress();

        // Update active page.
        UpdatePages();
    }


    public void UpdatePages()
    {
        // Makes any page other than current invisible and makes current visible
        foreach (Transform child in transform)
        {
            int index = child.GetSiblingIndex();
            if (index != _currentPage)
            {
                child.gameObject.SetActive(false);
            }
            else
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    
    /**
     * Save progress.
     */
    private void SaveProgress()
    {
        _progress = _currentPage;
        string currentDir = Application.persistentDataPath;        
        string fileName = _bookChoices + "envStatus.txt";
        string fullPath = currentDir + "/" + fileName;
        string bookStatus = "Progress:" + _progress;
        try
        {
            File.WriteAllText(fullPath, bookStatus);
        }
        catch (Exception e)
        {
            // Extract some information from this exception, and then   
            // throw it to the parent method.  
            if (e.Source != null)  
                Console.WriteLine("Exception source: {0}", e.Source);  
            throw;  
        }
    }

    private void LoadProgress()
    {
        // The path of the txt file storing game status.
        string currentDir = Application.persistentDataPath;
        string fileName = _bookChoices + "envStatus.txt";
        string fullPath = currentDir + "/" + fileName;
        try
        {
            int bookStatus;
            // Try reading the progress.
            if (!Int32.TryParse(File.ReadAllText(fullPath).Split(':')[1], out bookStatus))
            {
                // Reset everything.
                _progress = 0;
                _currentPage = 0;
            }
            else
            {
                // Reload game.
                _progress = bookStatus;
                _currentPage = _progress;
            }
        }
        catch (Exception e)
        {
            // Create an empty file.
            File.Create(fullPath).Dispose();
            _progress = 0;
            _currentPage = 0;
        }
        
    }


    void Start()
    {
        // The path of the txt file to store game status.
        string currentDir = Application.persistentDataPath;
        string fileName = "envStatus.txt";
        string fullPath = currentDir + "/" + fileName;
        try
        {
            int bookStatus;
            // Try reading the progress.
            if (!Int32.TryParse(File.ReadAllText(fullPath).Split(':')[1], out bookStatus))
            {
                // Reset everything.
                _progress = 0;
                _currentPage = 0;
            }
            else
            {
                // Reload game.
                _progress = bookStatus;
                _currentPage = _progress;
            }
        }
        catch (Exception e)
        {
            // Create an empty file.
            File.Create(fullPath);
            _progress = 0;
            _currentPage = 0;
        }
        
       
            // Sets all children as invsible other than current
            foreach (Transform child in transform)
            {
                // child is your child transform
                int index = child.GetSiblingIndex();
                if (index != _progress)
                {
                    child.gameObject.SetActive(false);
                }
            }
      
    }
}