using TMPro;
using UnityEngine;

public class ActionLogManager : MonoBehaviour
{
    [SerializeField] TextMeshPro actionLogText;

    private void Awake()
    {
        if (actionLogText == null)
        {
            Debug.LogError("ActionLogManager: ActionLogText not found in children!");
        }
    }

    public void AddLogEntry(string entry)
    {
        Debug.Log($"ActionLogManager.AddLogEntry: Adding log entry: {entry}");
        if (actionLogText != null)
        {
            actionLogText.text = entry; // Replace the existing text
        }
        else
        {
            Debug.LogError("ActionLogManager: actionLogText is null!");
        }
        Debug.Log($"ActionLogManager.AddLogEntry: Added log entry.");
    }
}