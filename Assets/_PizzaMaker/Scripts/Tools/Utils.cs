using System;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;

public static class Utils
{
    public static List<Conversation> GetConversationsByActorName(string actorName, DialogueDatabase dialogueDatabase, string conversationFilterPrefix)
    {
        var conversations = new List<Conversation>();
        var actor = dialogueDatabase.GetActor(actorName);
        if (actor != null)
        {
            foreach (var conversation in dialogueDatabase.conversations)
            {
                if (conversation.Title.StartsWith(conversationFilterPrefix, StringComparison.OrdinalIgnoreCase))
                {
                    conversations.Add(conversation);
                }
            }
        }

        return conversations;
    }

    public static void SetGameLayerRecursive(this GameObject _go, int _layer)
    {
        _go.layer = _layer;
        foreach (Transform child in _go.transform)
        {
            child.gameObject.layer = _layer;

            Transform _HasChildren = child.GetComponentInChildren<Transform>();
            if (_HasChildren != null)
                SetGameLayerRecursive(child.gameObject, _layer);
        }
    }
}