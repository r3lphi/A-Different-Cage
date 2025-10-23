using UnityEngine;

public class Sanitiser : Item
{
    public AudioClip explosionSound;
    public override void Use()
    {
        player.Kill();
        player.GetComponent<AudioSource>().PlayOneShot(explosionSound);
        playerHoldingHand.DeleteHeldItem();
    }
}
