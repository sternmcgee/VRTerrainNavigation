using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTypes : MonoBehaviour
{
    [SerializeField]
    private List<string> typeNames = null;
    [SerializeField]
    private List<Material> typeImages = null;
    public Dictionary<string, Material> types = null;

    // Start is called before the first frame update
    void Start()
    {
        if (typeNames == null)
            Debug.Log("Error: The type names list is empty on RockTypes script.");
        if (typeImages == null)
            Debug.Log("Error: The type names list is empty on RockTypes script.");
        if (typeNames.Count != typeImages.Count)
            Debug.Log("Error: The type names and type images lists do not have the same length on RockTypes script.");

        types = new Dictionary<string, Material>();
        for (int i = 0; i < typeNames.Count; i++)
        {
            types.Add(typeNames[i], typeImages[i]);
        }
    }

    //return whether the string is a stored rock type
    public bool IsRockType(string typeName)
    {
        return types.ContainsKey(typeName);
    }

    //return the image corresponding to the rock type name
    public Material GetImage(string typeName)
    {
        return types[typeName];
    }
}
