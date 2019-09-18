using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using MessageBusLib;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private const string MOVEMENT_CONTROLLER_FILTER = "Movement Controller";

    public float MoveSpeed = 1;

    private Rigidbody2D _rigidBody;
    private Vector2 _direction = Vector2.zero;

    void Awake()
    {
        _rigidBody = transform.parent.GetComponent<Rigidbody2D>();
        SubscribeToMessages();
    }

    // Update is called once per frame
    void Update()
    {
        if (_direction != Vector2.zero)
        {
            var newPosition = _rigidBody.position;
            newPosition += _direction * (MoveSpeed * Time.deltaTime);
            _rigidBody.MovePosition(newPosition);
        }
    }

    private void SubscribeToMessages()
    {
        //We only want to receive the UpdateDireciton message if it's sent to us on the specific filter OR it's sent directly to the parent object;
        transform.parent.gameObject.SubscribeWithFilter<UpdateDirectionMessage>(UpdateDirection, MOVEMENT_CONTROLLER_FILTER);
    }

    private void UpdateDirection(UpdateDirectionMessage msg)
    {
        _direction = msg.Direction;
    }

    void OnDestroy()
    {
        gameObject.UnsubscribeFromAllMessagesWithFilter(MOVEMENT_CONTROLLER_FILTER);
    }
}
