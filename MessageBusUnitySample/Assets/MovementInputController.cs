using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using MessageBusLib;
using UnityEngine;

public class MovementInputController : MonoBehaviour
{
    private static string MOVEMENT_INPUT_FILTER = "Movement Input";

    private Vector2 _direction = Vector2.zero;

    void Awake()
    {
        SubscribeToMessages();
    }

    private void SubscribeToMessages()
    {
        gameObject.Subscribe<UpdateInputStateMessage>(UpdateInputState);
    }

    private void UpdateInputState(UpdateInputStateMessage msg)
    {
        var direction = Vector2.zero;
        if (msg.Current.Up)
        {
            direction.y = 1;
        }
        else if (msg.Current.Down)
        {
            direction.y = -1;
        }
        if (msg.Current.Left)
        {
            direction.x = -1;
        }
        else if (msg.Current.Right)
        {
            direction.x = 1;
        }
        if (direction != _direction)
        {
            gameObject.SendMessageTo(new UpdateDirectionMessage{Direction = direction}, transform.parent.gameObject);
            _direction = direction;
        }
    }

    void OnDestroy()
    {
        transform.parent.gameObject.UnsubscribeFromAllMessagesWithFilter(MOVEMENT_INPUT_FILTER);
    }
}
