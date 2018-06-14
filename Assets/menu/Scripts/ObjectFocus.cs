using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class ObjectFocus : MonoBehaviour
{

    [Serializable]
    public class FloatEvent : UnityEvent<float> { }

    [SerializeField] Transform reference;

    [SerializeField] float minAngle = 10;
    [SerializeField] float maxAngle = 30;

    [SerializeField] AnimationCurve curve;
    [SerializeField] FloatEvent curveValueChanged;
    [SerializeField] FloatEvent rawValueChanged;
    [SerializeField] UnityEvent onGotFocus;
    [SerializeField] UnityEvent onLostFocus;

    private float _fadeAmount = -1;
    public float fadeAmount
    {
        get { return _fadeAmount; }
        set
        {
            if (value != _fadeAmount)
            {
                _fadeAmount = value;
                curve.Evaluate(_fadeAmount);
                curveValueChanged.Invoke(curve.Evaluate(_fadeAmount));
                rawValueChanged.Invoke(_fadeAmount);

            }
        }
    }

    private float _delta = -1;
    public float delta
    {
        get { return _delta; }
        set
        {
            if (value != _delta)
            {
                _delta = value;
                fadeAmount = Mathf.InverseLerp(maxAngle, minAngle, _delta);

                if (_delta <= minAngle)
                    ObjectFocusManager.Add(this);
                else
                    ObjectFocusManager.Remove(this);
            }
        }
    }

    public void GotFocus()
    {
        Debug.Log(gameObject.name + " got focus");
        onGotFocus.Invoke();
    }

    public void LostFocus()
    {
        Debug.Log(gameObject.name + " lost focus");
        onLostFocus.Invoke();

    }

    void Fade()
    {
        //Debug.DrawLine(reference.position, transform.position,
        //               Color.yellow);
        //Debug.DrawRay(reference.position, reference.forward, Color.cyan);

        Vector3 d = (transform.position - reference.position).normalized;
        Debug.DrawRay(reference.position, d, Color.yellow);
        Debug.DrawRay(reference.position, reference.forward, Color.cyan);

        delta = Vector3.Angle(d.normalized, reference.forward);
    }

    // Use this for initialization
    void Awake()
    {
        if (!reference)
        {
            reference = Camera.main.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Fade();

    }

    //#if DEBUG
    //void OnGUI()
    //{
    //    GUILayout.Label("delta: " + delta.ToString());
    //    GUILayout.Label("fadeAmount: " + fadeAmount.ToString());
    //}
    //#endif
}
