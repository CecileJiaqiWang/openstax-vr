﻿// Creates a system for viewing a set of images all grouped under one parent gameObject
// Create two button (forward, backward) and attached them to the parent gameObject 

using System;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

    // Dummy Contructor.
    private BookLogic()
    {
        _currentId = 0;
    }

    private int GetNextId()
    {
        return ++_currentId;
    }

    /**
     * Get total number of pages.
     */
    private int TotalPageCount()
    {
        return transform.childCount;
    }

    /**
     * Display the next page.
     */
    public void Next()
    {
        // Updates the current page by moving forward one page
        CurrentPage = TotalPageCount() > CurrentPage + 1 ? CurrentPage + 1 : 0;

        // Save progress if in autosave mode.
        if (AutoSave)
        {
            SaveProgress();
        }

        UpdatePages();
    }

    /**
     * Display the previous page.
     */
    public void Back()
    {
        // Updates the current page by moving back one page
        CurrentPage = CurrentPage > 0 ? CurrentPage - 1 : TotalPageCount() - 1;

        // Save progress if in autosave mode.
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

        // Save progress if in autosave mode.
        if (AutoSave)
        {
            SaveProgress();
        }

        UpdatePages();
    }

    /**
     * Erase progress.
     */
    public void EraseProgress()
    {
        Progress = 0;
        CurrentPage = 0;
        UpdatePages();
    }

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
    //ToDO
    public void SaveProgress()
    {
        Progress = CurrentPage;
    }

    /*
     * Enable / disable autosave.
     */
    public void ToggleAutoSave()
    {
        AutoSave = !AutoSave;
        Debug.Log("Autosave status: " + AutoSave);
    }


    //Todo: Hide other objects.
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