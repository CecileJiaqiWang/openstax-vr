using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Renderer))]
public class MaterialGradientModifier : MonoBehaviour {
    Renderer _renderer;

    //public Color myColor;
    [SerializeField] Gradient gradient;

    float _gradientPosition = -1;

    public float gradientPosition
    {
        get { return _gradientPosition; }
        set
        {
            if (value != _gradientPosition)
            {
                _gradientPosition = value;
                _renderer.material.color = gradient.Evaluate(_gradientPosition);

            }
        }
    }

    void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }


    //// Use this for initialization
    //void Start () {
    //    gradientPosition = 0;
        
    //}
    
 //   // Update is called once per frame
 //   void Update () {
 //       gradientPosition = Mathf.Sin(Time.time * 0.5f) + 0.5f;
 //       //_renderer.material.color = gradient.Evaluate(gradientPosition);
	//}
}
