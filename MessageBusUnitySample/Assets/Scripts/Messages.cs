using System.Numerics;
using MessageBusLib;
using Vector2 = UnityEngine.Vector2;

namespace Assets.Scripts
{
    //One place for all message classes. No reason to split them up when we're only working locally. May want to split them up eventually if you have multiplayer messages, or if you want to get detailed, UI messages
    // I prefer on place for them all though

    //EventMessage class includes a Sender and Receiver object that is automatically attached based on who is sending the message   

    //A message can be a lot of things - in this case, we're going to be broadcasting an InputStateMessage to objects who subscribe to it. We only send to objects who are subscribed and no one else
    public class UpdateInputStateMessage : EventMessage
    {
        public InputState Previous { get; set; }
        public InputState Current { get; set; }
    }

    public class UpdateDirectionMessage : EventMessage
    {
        public Vector2 Direction { get; set; }
    }
}