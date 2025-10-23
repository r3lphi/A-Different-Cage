using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public Vector3 heldPosition;
    public Vector3 heldRotation;
    public Vector2 heldScale;
    public UnityEvent onPickup;
    public bool[] activeForLevels;

    protected PlayerController player;
    protected MessageManager messageManager;
    protected Hand playerHoldingHand;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        messageManager = FindObjectOfType<MessageManager>();
        playerHoldingHand = FindObjectOfType<Hand>();
    }

    public virtual void Use()
    {
    }
}
