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
        return transform.childCount;
    }

    /**
     * Displays the next page.
     */
    public void Next()
    {
        // Updates the current page by moving forward one page
        _currentPage = TotalPageCount() > _currentPage + 1 ? _currentPage + 1 : 0;

        SaveProgress();

        UpdatePages();
    }

    /**
     * Displays the previous page.
     */
    public void Back()
    {
        // Updates the current page by moving back one page
        _currentPage = _currentPage > 0 ? _currentPage - 1 : TotalPageCount() - 1;

        SaveProgress();

        UpdatePages();
    }

    public void GoTo(int pageNum)
    {
        // Given an input desired page number in unity, goes to that specific page
        _currentPage = pageNum;
        
        SaveProgress();

        UpdatePages();
    }

    /**
     * Erases progress.
     */
    public void EraseProgress()
    {
        _progress = 0;
        string currentDir = Directory.GetCurrentDirectory();
        string fileName = "envStatus.txt";
        string fullPath = currentDir + "/" + fileName;
        string bookStatus = "Progress:" + _progress;
        try
        {
            File.WriteAllText(fullPath, bookStatus);
            Debug.Log("Erased progress!");
            _currentPage = _progress;
            UpdatePages();
        }
        catch (Exception e)
        {
            Debug.Log("Failed to erase progress!");
        }
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
    public void SaveProgress()
    {
        _progress = _currentPage;
        string currentDir = Directory.GetCurrentDirectory();
        string fileName = "envStatus.txt";
        string fullPath = currentDir + "/" + fileName;
        string bookStatus = "Progress:" + _progress;
        try
        {
            File.WriteAllText(fullPath, bookStatus);
            Debug.Log("Saved!");
        }
        catch (Exception e)
        {
            Debug.Log("Failed to save progress!");
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