using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using MessageBusLib;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public KeyCode Up = KeyCode.W;
    public KeyCode Down = KeyCode.S;
    public KeyCode Left = KeyCode.A;
    public KeyCode Right = KeyCode.D;

    private InputState _previousState = new InputState();

    void Update()
    {
        var current = new InputState
        {
            Up = Input.GetKey(Up),
            Down = Input.GetKey(Down),
            Left = Input.GetKey(Left),
            Right = Input.GetKey(Right)
        };
        //In this case, we're telling the message bus to send this message to anyone who subscribes to UpdateInputState without a filter
        gameObject.SendMessage(new UpdateInputStateMessage{Previous = _previousState, Current = current});
    }
}
