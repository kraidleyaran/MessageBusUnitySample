using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using MessageBusLib;
using UnityEngine;
using UnityEngine.Networking;

public class AnimationController : MonoBehaviour
{
    private const string ANIMATION_CONTROLLER_FILTER = "Animation Controller";

    private const string X_PARAMETER = "X";
    private const string Y_PARAMETER = "Y";
    

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        SubscribeToMessages();
        _animator = transform.GetComponent<Animator>();
        _spriteRenderer = transform.GetComponent<SpriteRenderer>();
    }

    private void SubscribeToMessages()
    {
        // We can subscribe to object with a filter, but if we send the object a specific message, the filter is bypassed.
        // This allows us to subscribe multiple messages to a single filter, then in the OnDestroy method, we can unsubcribe from all messages with that given filter
        // This leaves our other child components alone, but allows us to respond to a single message sent to the parent
        // In this case, the animation controller responds to UpdateDirection when it's sent to the parent object via the MovementInputController
        transform.parent.gameObject.SubscribeWithFilter<UpdateDirectionMessage>(UpdateDirection, ANIMATION_CONTROLLER_FILTER);
    }

    private void UpdateDirection(UpdateDirectionMessage msg)
    {
        if (msg.Direction != Vector2.zero)
        {
            _spriteRenderer.flipX = msg.Direction.x > 0;
            _animator.SetFloat(X_PARAMETER, msg.Direction.x);
            _animator.SetFloat(Y_PARAMETER, msg.Direction.y);
        }
    }

    void OnDestroy()
    {
        gameObject.UnsubscribeFromAllMessagesWithFilter(ANIMATION_CONTROLLER_FILTER);
    }
}
