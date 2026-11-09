using UnityEngine;

public class Drawer : MonoBehaviour
{
    public float openDisplacement;
    public float closedDisplacement;
    private bool isOpen = false;
    public void Open()
    {
        isOpen = true;
        transform.localPosition = new Vector3(
            transform.localPosition.x,
            transform.localPosition.y,
            openDisplacement
        );
    }
    public void Close()
    {
        isOpen = false;
        transform.localPosition = new Vector3(
            transform.localPosition.x,
            transform.localPosition.y,
            closedDisplacement
        );
    }
    public void Toggle()
    {
        if (isOpen)
        {
            Close();
        }
        else
        {
            Open();
        }
    }
}