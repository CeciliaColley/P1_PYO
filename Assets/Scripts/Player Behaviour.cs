using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    void Awake()
    {
        gameManager.activePlayers.Add(gameObject);
    }
}
