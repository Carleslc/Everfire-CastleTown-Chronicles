using UnityEngine;
using System.Collections;

public interface ITalkable {
    DialogueTree LoadTree();
    void ProcessCommands(DialogueCommand[] commands);        
}
