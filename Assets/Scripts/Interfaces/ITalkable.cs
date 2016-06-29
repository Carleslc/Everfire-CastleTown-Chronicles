using UnityEngine;
using System.Collections;

public interface ITalkable {
    void StartTalking();
    void StopTalking();
    DialogueTree LoadTree();
    void ProcessCommands(DialogueCommand[] commands);  
}
