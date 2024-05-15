using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationRetriever : MonoBehaviour
{
    [SerializeField] private string boardInformationSOName;
    public BoardInformation boardInformation;

    private static InformationRetriever instance;
    public static InformationRetriever Instance => instance ?? (instance = FindFirstObjectByType<InformationRetriever>());

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        GetBoardInformation();
    }

    public void GetBoardInformation()
    {
        if (boardInformation != null)
        {
            Destroy(boardInformation);
        }
        var reference = Resources.Load<BoardInformation>("Scriptable Objects/" + boardInformationSOName);
        boardInformation = reference;
    }
}
