// Creates a system for viewing a set of images all grouped under one parent gameObject
// Create two button (forward, backward) and attached them to the parent gameObject 

using System;
using UnityEngine;


public class BookLogic : MonoBehaviour
{
    // Properties.
    private int CurrentPage { get; set; }
    private int Progress { get; set; }
    private int Id { get; set; }
    private string Title { get; set; }
    private string Tag { get; set; }
    private DateTime LastOpenDate { get; set; }

    private static int _currentId;
    public bool AutoSave = true;
    public bool HideBook = false;

    // Instance Constructor.
    public BookLogic(string title, string tag)
    {
        this.Id = GetNextId();
        this.Title = title;
        this.Tag = tag;
    }

    // Dummy Constructor.
    private BookLogic()
    {
        _currentId = 0;
    }

    private int GetNextId()
    {
        return ++_currentId;
    }

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
        CurrentPage = TotalPageCount() > CurrentPage + 1 ? CurrentPage + 1 : 0;

        // Saves progress if in autosave mode.
        if (AutoSave)
        {
            SaveProgress();
        }

        UpdatePages();
    }

    /**
     * Displays the previous page.
     */
    public void Back()
    {
        // Updates the current page by moving back one page
        CurrentPage = CurrentPage > 0 ? CurrentPage - 1 : TotalPageCount() - 1;

        // Saves progress if in autosave mode.
        if (AutoSave)
        {
            SaveProgress();
        }

        UpdatePages();
    }

    public void GoTo(int pageNum)
    {
        // Given an input desired page number in unity, goes to that specific page
        CurrentPage = pageNum;

        // Saves progress if in autosave mode.
        if (AutoSave)
        {
            SaveProgress();
        }

        UpdatePages();
    }

    /**
     * Erases progress.
     */
    public void EraseProgress()
    {
        Progress = 0;
        CurrentPage = 0;
        UpdatePages();
    }

    /*
     * Hides / displays the book.
     */

    public void Hide()
    {
        HideBook = !HideBook;
        if (HideBook)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        else
        {
            UpdatePages();
        }
    }

    public void UpdatePages()
    {
        // Makes any page other than current invisible and makes current visible
        foreach (Transform child in transform)
        {
            int index = child.GetSiblingIndex();
            if (index != CurrentPage)
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
    //ToDo
    public void SaveProgress()
    {
        Progress = CurrentPage;
    }

    /*
     * Enables / disables autosave.
     */
    //ToDO
    public void ToggleAutoSave()
    {
        AutoSave = !AutoSave;
        Debug.Log("Autosave status: " + AutoSave);
    }


    void Start()
    {
        if (!HideBook)
        {
            // Sets all children as invsible other than current
            foreach (Transform child in transform)
            {
                // child is your child transform
                int index = child.GetSiblingIndex();
                if (index != Progress)
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}