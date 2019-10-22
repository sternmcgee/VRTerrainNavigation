using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

namespace Valve.VR.InteractionSystem.Sample
{
    public class UIAppear : MonoBehaviour
    {
        // "SerializeField" makes it visible in the inspector without being a public variable
        public Image customImage;
        public Canvas ControlPanel;
        private Interactable interactable;
        public float delay = 0.1f;
        public bool showing = false;// Are we close enough to start the animation
        protected Animator[] children;

        /*
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) // If the player is found
            {
                ControlPanel.enabled = true;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                ControlPanel.enabled = false;
            }
        }
        */
        // Start is called before the first frame update
        void Start()
        {
            interactable = GetComponent<Interactable>();
            //interactable = GetComponentInParent<Interactable>();
            children = GetComponentsInChildren<Animator>();
            for (int aa = 0; aa < children.Length; aa++)
            {
                children[aa].SetBool("Shown", showing);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (interactable.attachedToHand)
            {
                ControlPanel.enabled = true;
                if (showing) return;
                StartCoroutine("ActivateInTurn");
            }
            else
            {
                ControlPanel.enabled = false;
                if (!showing) return;
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
}