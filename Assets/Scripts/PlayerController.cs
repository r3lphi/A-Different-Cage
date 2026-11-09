using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInput input;
    public Transform camera;

    [Header("Mouse")]
    public float cameraSensitivity;

    [Header("Movement")]
    public float originalMoveSpeed;
    public float moveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        moveSpeed = originalMoveSpeed;
    }

    private Vector3 cameraRot;
    public void SetCameraRot(Vector3 rot){
        cameraRot = rot;
    }

    void Update()
    {
        //Camera Movement
        Vector2 lookInput = input.actions["Look"].ReadValue<Vector2>();
        cameraRot += new Vector3(-lookInput.y, lookInput.x, 0) * cameraSensitivity * Time.deltaTime;
        cameraRot.x = Mathf.Clamp(cameraRot.x, -89, 89);

        camera.localEulerAngles = cameraRot;

        //Interactable calls
        CheckForInteractable();

    }
    void FixedUpdate()
    {
        //Rigidbody Movement
        Move();
    }

    public Rigidbody rb;
    public CapsuleCollider collider;

    void Move()
    {
        Vector2 moveInput = input.actions["Move"].ReadValue<Vector2>();

        Vector3 moveVec = camera.forward * moveInput.y + camera.right * moveInput.x;
        moveVec.y = rb.linearVelocity.y;

        rb.linearVelocity = moveVec.normalized * moveSpeed;
    }

    [Header("Item System")]
    public Hand holdingHand;
    public Color interactColor;
    // public LayerMask InteractableDetectionMask;
    public float maxInteractDistance;
    private Item itemInView;
    [SerializeField]
    private Interactable interactableCoveredByCursor;

    public void OnInteract()
    {
        if (interactableCoveredByCursor != null){
            interactableCoveredByCursor.onInteract.Invoke();
        }
    }
    public void OnUse()
    {
        if (holdingHand.heldItem != null)
        {
            holdingHand.heldItem.Use();
        }
    }
    public void OnDrop()
    {
        holdingHand.DropItem();
    }

    void CheckForInteractable()
    {
        //Check if there's an object in front of player
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxInteractDistance))
        {
            // print(hit.transform.gameObject.name);
            if (hit.transform.GetComponent<Interactable>() == null) {
                if (interactableCoveredByCursor != null){
                    interactableCoveredByCursor.Delight();
                    interactableCoveredByCursor = null;
                }

                return;
            }
            interactableCoveredByCursor = hit.transform.GetComponent<Interactable>();
            interactableCoveredByCursor.Highlight();
        }
    }

    public void MultiplySpeedBy(float multipler)
    {
        moveSpeed *= multipler;
    }

    public void Kill()
    {
        GetComponent<TakeDrugEffects>().TakeDrug("You died.");
    }

    [Header("Status")]
    public int pillsTaken = 0;
}
