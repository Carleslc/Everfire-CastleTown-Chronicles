using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class DebugLogger : MonoBehaviour{

    private Dictionary<DebugChannel, bool> channelsDictionary;
    public MutedChannel[] channels;
    public List<string> tags;
    private static DebugLogger debugLogger;

    public static DebugLogger instance
    {
        get
        {
            if (!debugLogger)
            {
                debugLogger = FindObjectOfType(typeof(DebugLogger)) as DebugLogger;

                if (!debugLogger)
                {
                    Debug.LogError("There needs to be one active DebugLogger script on a GameObject in your scene.");
                }
                else
                {
                    debugLogger.Init();
                }
            }
            return debugLogger;
        }
    }

    
    void Init() {
        channelsDictionary = new Dictionary<DebugChannel, bool>();
        Array valuesArray = Enum.GetValues(typeof(DebugChannel));
        channels = new MutedChannel[valuesArray.Length];
        int i = 0;
        foreach (DebugChannel dc in valuesArray) {
            channels[i] = new MutedChannel(dc, true);
            channelsDictionary.Add(dc, true);
            ++i;
        }
        tags = new List<string>();
    }

    private void SyncDictionary() {
        foreach (MutedChannel mc in channels) {
            channelsDictionary[mc.type] = mc.muted;
        }
    }

    public static void Log(DebugChannel dc, string message) {
        instance.SyncDictionary();
        if (!instance.channelsDictionary[dc]) {
            Debug.Log("[" + dc + "]: " + message);
        }
    }

    public static void Log(DebugChannel dc, string message, string tag) {
        bool log = false;
        instance.SyncDictionary();
        if (!instance.channelsDictionary[dc])
        {
            log = true;
        }
        else {
            if (instance.tags.Contains(tag))
                log = true;
        }
        if (log) {
            Log(dc, message + " T: " + tag);
        }
    }

    [System.Serializable]
    public struct MutedChannel {
        public DebugChannel type;
        public bool muted;
        public MutedChannel(DebugChannel type, bool muted) {
            this.type = type;
            this.muted = muted;
        }
    }

}

public enum DebugChannel {
    World, Village, Villager, Entity
}
