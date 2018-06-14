using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ChangeMaterial : MonoBehaviour
{
    private Renderer myRenderer;

    public Material inactiveMaterial;
    public Material gazedAtMaterial;

    void Start()
    {
        myRenderer = GetComponent<Renderer>();
        SetGazedAt(false);
    }

    public void SetGazedAt(bool gazedAt)
    {
        if (inactiveMaterial != null && gazedAtMaterial != null)
        {
            myRenderer.material = gazedAt ? gazedAtMaterial : inactiveMaterial;
        }
    }
}