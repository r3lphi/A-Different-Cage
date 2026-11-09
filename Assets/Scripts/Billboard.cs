using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform player;
    public float slerpSpeed;

    void Update()
    {
        SpriteRenderer[] sprites = FindObjectsOfType<SpriteRenderer>();
        foreach(SpriteRenderer sprite in sprites)
        {
            // var targetRot = Quaternion.LookRotation(player.transform.position - sprite.transform.position);
            // sprite.transform.rotation = Quaternion.Slerp(sprite.transform.rotation, targetRot, slerpSpeed * Time.deltaTime);
            sprite.transform.LookAt(player);
            sprite.transform.localRotation = Quaternion.Euler(
                0,
                sprite.transform.localRotation.y,
                sprite.transform.localRotation.z
            );
        }
    }
}
