using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockTrigger : MonoBehaviour
{
    //the type name of the rock (e.g. calcite)
    [SerializeField]
    private string rockType;
    //the gameobject reference to the UI display object
    [SerializeField]
    private GameObject uiDisplay;
    //reference to RockTypes script
    [SerializeField]
    private RockTypes rockTypes;

    // Start is called before the first frame update
    void Start()
    {
        if (rockType == null)
            Debug.Log("Error: No rock type has been assigned to this object.");
        if (uiDisplay == null)
            Debug.Log("Error: No rock display UI has been assigned to this object.");
        if (rockTypes == null)
            rockTypes = (RockTypes)GameObject.FindObjectOfType(typeof(RockTypes));
        if (rockTypes == null)
            Debug.Log("Error: No RockTypes script has been found in scene to reference.");
    }

    private void UpdateRockDisplay()
    {
        if (!rockTypes.IsRockType(rockType))
        {
            Debug.Log("Error: Name of rock type is not recognized as a stored rock type.");
            return;
        }

        Image displayImage = uiDisplay.GetComponentInChildren<Image>();
        displayImage.material = rockTypes.GetImage(rockType);
        Debug.Log("Rock material updated! used " + rockType);
    }

    // Function to Make the Rock Display Invisible
    private void RemoveRockDisplay()
    {
        if (!rockTypes.IsRockType(rockType))
        {
            Debug.Log("Error: Name of rock type is not recognized as a stored rock type.");
            return;
        }

        Image displayImage = uiDisplay.GetComponentInChildren<Image>();
        displayImage.material = rockTypes.GetImage("default");
        Debug.Log("Rock material updated! used " + rockType);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Rover" || other.tag == "Player")
        {
            Debug.Log("Trigger collided!");
            UpdateRockDisplay();
        }
    }

    // Person or Rover has Exited the Rocks Sphere of Influence
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Rover" || other.tag == "Player")
        {
            Debug.Log("Trigger exited!");
            RemoveRockDisplay();
        }
    }
}
