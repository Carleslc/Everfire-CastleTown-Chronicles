using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
/// <summary>
/// This class works like this:
///     There are a number of muted channels. Those channels are all the enum values of DebugChannels.
///     When a channel is muted, no Log from this channel will make its way through except if its tag equals
///     one of the tags in the allowedTags structure.
/// </summary>
[ExecuteInEditMode]
public class DebugLogger : MonoBehaviour
{
    private Dictionary<DebugChannel, bool> mutedChannels;
    /// <summary>
    /// This is ONLY to make it possible to be shown in the Editor.
    /// </summary>
    public MutedChannel[] mutedChannelsEditor;
    public List<string> allowedTags;

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

    private void Hello() { }

    void OnEnable()
    {
        //We need to call this shit
        instance.Hello();
    }

    //We store everything back in PlayerPrefs
    void OnDisable()
    {
        SyncDictionary();
        foreach (KeyValuePair<DebugChannel, bool> pair in instance.mutedChannels)
        {
            string prefsKey = pair.Key.ToString();
            if (pair.Value)
                PlayerPrefs.SetInt(prefsKey, 1);
            else
            {
                PlayerPrefs.SetInt(prefsKey, 0);
            }
        }
        string[] tagsArray = instance.allowedTags.ToArray();
        //We store our tags...
        for (int i = 0; i < tagsArray.Length; i++)
        {
            string key = "tags" + i.ToString();
            PlayerPrefs.SetString(key, tagsArray[i]);
        }
        //And if the allowedTags structure has diminished in size, we supress further data.
        bool finished = false;
        for (int j = tagsArray.Length; !finished; j++)
        {
            string key = "tags" + j.ToString();
            if (PlayerPrefs.HasKey(key))
            {
                PlayerPrefs.DeleteKey(key);
            }
            else
                finished = true;
        }
    }

    void Init()
    {
        instance.mutedChannels = new Dictionary<DebugChannel, bool>();
        Array debugChannels = Enum.GetValues(typeof(DebugChannel));
        instance.mutedChannelsEditor = new MutedChannel[debugChannels.Length];
        int i = 0;
        foreach (DebugChannel dc in debugChannels)
        {
            bool value = true;
            if (PlayerPrefs.HasKey(dc.ToString()))
            {
                if (PlayerPrefs.GetInt(dc.ToString()) != 1)
                {
                    value = false;
                }
            }
            instance.mutedChannelsEditor[i] = new MutedChannel(dc, value);
            instance.mutedChannels.Add(dc, value);
            ++i;
        }
        instance.allowedTags = new List<string>();
        bool finished = false;
        for (int j = 0; !finished; j++)
        {
            string key = "tags" + j.ToString();
            if (PlayerPrefs.HasKey(key))
            {
                instance.allowedTags.Add(PlayerPrefs.GetString(key));
            }
            else
                finished = true;
        }
    }

    private void SyncDictionary()
    {
        foreach (MutedChannel mc in instance.mutedChannelsEditor)
        {
            instance.mutedChannels[mc.type] = mc.muted;
        }
    }

    public static void Log(DebugChannel dc, string message)
    {
        instance.SyncDictionary();
        if (!instance.mutedChannels[dc])
        {
            Debug.Log("[" + dc + "]: " + message);
        }
    }

    public static void Log(DebugChannel dc, string message, string tag)
    {
        instance.SyncDictionary();
        bool log = false;
        if (!instance.mutedChannels[dc])
        {
            log = true;
        }
        else
        {
            if (instance.allowedTags.Contains(tag)) {
                log = true;
            }

        }
        if (log)
        {
            Debug.Log("[" + dc + "]: " + message + " T: " + tag);
        }
    }

    [System.Serializable]
    public struct MutedChannel
    {
        public DebugChannel type;
        public bool muted;
        public MutedChannel(DebugChannel type, bool muted)
        {
            this.type = type;
            this.muted = muted;
        }
    }

}

public enum DebugChannel
{
    World, Village, Worker, Entity, Movement

}
