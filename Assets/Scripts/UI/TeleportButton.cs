//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Demonstrates the use of the controller hint system
//
//=============================================================================

using UnityEngine;
using System.Collections;
using Valve.VR;

namespace Valve.VR.InteractionSystem.Sample
{
    //-------------------------------------------------------------------------
    public class TeleportButton : MonoBehaviour
    {
        private Coroutine buttonHintCoroutine;
        private Coroutine textHintCoroutine;
        public GameObject player = null;
        public Transform teleportPoint = null;

        private void Start()
        {
            if (player == null)
                Debug.LogError("Error: Player gameobject reference not assigned to script.");
            if (teleportPoint == null)
                Debug.LogError("Error: Teleport Point transform not assigned to script.");                
        }

        //-------------------------------------------------
        public void TeleportToPoint()
        {
            if (teleportPoint != null && player != null)
            {
                SteamVR_Fade.View(Color.black, 0);
                SteamVR_Fade.View(Color.clear, 1);
                player.transform.position = teleportPoint.transform.position;
                player.transform.rotation = teleportPoint.transform.rotation;
            }
        }
    }
}
