using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockTrigger : MonoBehaviour
{
    public static class RockTypes
    {
        public const string calcite = "Materials/Rock Data/Calcite_Rock_Mat";
        public const string olivine = "Materials/Rock Data/Olivine_Rock_Mat";
        //add more for more type options
    }

    public string rockType;
    public Image rockDisplay;

    // Start is called before the first frame update
    void Start()
    {
        rockType = RockTypes.calcite;
        if (rockType == null)
            Debug.Log("Error: No rock type has been assigned to this object.");
        if (rockDisplay == null)
            Debug.Log("Error: No rock display UI has been assigned to this object.");
    }

    private void UpdateRockDisplay()
    {
        rockDisplay.material = Resources.Load(rockType, typeof(Material)) as Material;
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
}
