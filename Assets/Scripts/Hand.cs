using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Hand : MonoBehaviour
{
    public RawImage renderer;
    public Texture OpenHand;
    public Texture ClosedHand;
    public GameObject HeldItemUI;
    public Animator anim;
    public Item queuedItemPickup;
    public Item heldItem;

    public AudioSource audioSrc;
    public AudioClip pickupItemSound;

    public LayerMask PlayerlessMask;

    public void QueueItemPickup(Item item)
    {
        queuedItemPickup = item;        
        anim.SetTrigger("Grab");
    }

    public void PickupItem()
    {
        if (heldItem != null)
        {
            DropItem();
        }

        if (queuedItemPickup == null)
        {
            return;
        }

        heldItem = queuedItemPickup;

        HeldItemUI.GetComponent<RawImage>().texture = queuedItemPickup.GetComponent<SpriteRenderer>().sprite.texture;

        if (!HeldItemUI.activeSelf)
        {
            HeldItemUI.SetActive(true);
        }

        RectTransform rect = HeldItemUI.GetComponent<RectTransform>();
        rect.localPosition = queuedItemPickup.heldPosition;
        rect.sizeDelta = queuedItemPickup.heldScale;
        rect.localRotation = Quaternion.Euler(queuedItemPickup.heldRotation);

        queuedItemPickup.gameObject.SetActive(false);
        queuedItemPickup = null;

        heldItem.onPickup.Invoke();

        audioSrc.PlayOneShot(pickupItemSound);

    }
    public void DropItem()
    {
        if (heldItem != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3f, PlayerlessMask) == null){
                return;
            }
            heldItem.transform.position = hit.point;
            heldItem.gameObject.SetActive(true);

            HeldItemUI.SetActive(false);
            if (queuedItemPickup == null)
            {
                Open();
            }
            heldItem = null;
        }
    }
    public void DeleteHeldItem()
    {
        HeldItemUI.SetActive(false);
        heldItem = null;
        Open();
    }

    public void Open()
    {
        renderer.texture = OpenHand;
    }
    public void Close()
    {
        renderer.texture = ClosedHand;
    }
}
