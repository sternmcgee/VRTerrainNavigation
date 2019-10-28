using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPickup_01 : MonoBehaviour {

    public float minDistance = 2; // How close we want to get
    public float delay = 0.1f;
    public bool showing = false;// Are we close enough to start the animation
    protected Animator[] children;

    // Start is called before the first frame update
    void Start() {
        children = GetComponentsInChildren<Animator>();
        for (int aa = 0; aa < children.Length; aa++)
        {
            children[aa].SetBool("Shown", showing);
        }
    }
    
    // Update is called once per frame
    void Update() {
        Vector3 delta = Camera.main.transform.position - transform.position; // The distance between the user and the UI Component
        if (delta.magnitude < minDistance) // If the user is within showing range
        {
            if (showing) return;
            StartCoroutine("ActivateInTurn");
        }
        else // The user is outside the showing range
        {
            if (! showing) return;
            StartCoroutine("DeactivateInTurn");
        }
    }
    public IEnumerator ActivateInTurn() // This will iterate through the UI components
        {
        showing = true;

        //yield return new WaitForSeconds(delay);
        for (int aa = 0; aa < children.Length; aa++)
        {
            children[aa].SetBool("Shown", true);
            yield return new WaitForSeconds(delay); // After "delay" seconds have passed, continue on

        }
    }
    public IEnumerator DeactivateInTurn() // This will iterate through the UI components
    {
        showing = false;

        //yield return new WaitForSeconds(delay);
        for (int aa = 0; aa < children.Length; aa++)
        {
            children[aa].SetBool("Shown", false);
            yield return new WaitForSeconds(delay); // After "delay" seconds have passed, continue on

        }
    }
}
