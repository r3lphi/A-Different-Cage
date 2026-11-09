using UnityEngine;
using UnityEngine.Events;


public class Interactable : MonoBehaviour
{
    public UnityEvent onInteract;
    public string interactMessage;

    private SpriteRenderer spriteRenderer;
    private Material initialMaterial;
    
    public void Start(){
        TryGetComponent<SpriteRenderer>(out spriteRenderer);
        if (spriteRenderer != null){
            initialMaterial = spriteRenderer.material;
        }
    }

    public void Highlight(){
        if (spriteRenderer == null){
            return;
        }
        spriteRenderer.material = Resources.Load("Materials/Interactable",typeof(Material)) as Material;
    }
    public void Delight(){
        if (spriteRenderer == null){
            return;
        }
        spriteRenderer.material = initialMaterial;
    }
}
