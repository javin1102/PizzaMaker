using System;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;

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
}
