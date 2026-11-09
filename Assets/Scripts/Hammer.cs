using Unity.VisualScripting;
using UnityEngine;

public class Hammer : Item
{
    public SpriteRenderer OriginalTv;
    public Sprite crackedVersion;
    public Sprite fixedVersion;

    public GameObject GlassShard;

    public float rayDistance = 3f;
    public LayerMask hitLayers;

    public override void Use()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, hitLayers))
        {
            if (hit.collider.CompareTag("TV"))
            {
                SpriteRenderer tvRenderer = hit.collider.GetComponent<SpriteRenderer>();

                if (tvRenderer != null)
                {
                    if (tvRenderer.sprite == crackedVersion)
                        tvRenderer.sprite = fixedVersion;
                    else
                        tvRenderer.sprite = crackedVersion;
                }
            }
        }

        GlassShard.SetActive(true);
    }
}