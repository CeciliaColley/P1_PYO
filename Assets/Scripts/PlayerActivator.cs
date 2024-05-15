using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

// Consider renaming this to turn tracker, or ActivePlayerReferencer
public class PlayerActivator : MonoBehaviour
{

    [SerializeField] private GameObject activePlayerBox;

    public GameObject activePlayer;
    public List<GameObject> activePlayers = new List<GameObject>(); // Referenced by Player Behaviour, so players can add themselves to the list.
    public int activePlayerIndex = 1;

    private void Start()
    {
        activePlayer = activePlayers.First();
        PlayerBehaviour playerBehaviour= activePlayer.GetComponent<PlayerBehaviour>();
        activePlayerBox.transform.position = playerBehaviour.canvas.transform.position;
    }

}
