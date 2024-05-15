using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

// Consider renaming this to turn tracker, or ActivePlayerReferencer
public class PlayerActivator : MonoBehaviour
{
    public GameObject activePlayer;
    public List<GameObject> activePlayers = new List<GameObject>(); // Referenced by Player Behaviour, so players can add themselves to the list.

    private void Start()
    {
        activePlayer = activePlayers.First();
    }

}
