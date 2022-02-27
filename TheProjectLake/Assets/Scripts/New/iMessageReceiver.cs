using System.Collections;
using UnityEngine;
using System;


namespace SoulsLike
{

    public enum MessageType
    {
        DAMAGED,
        DEAD
    }
    public interface iMessageReceiver
    {
        void OnReceiveMessage(MessageType type);

    }
}
