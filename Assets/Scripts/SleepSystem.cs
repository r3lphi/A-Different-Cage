using System.Collections;
using UnityEngine;

public class SleepSystem : MonoBehaviour
{
    public bool isSleeping;
    public Transform sleepCameraAt;

    public PlayerController player;

    public void Interacted()
    {
        if (isSleeping)
        {
            LeaveBed();
        }
        else
        {
            EnterBed();
        }
    }

    public void EnterBed()
    {
        player.rb.isKinematic = true;
        player.collider.enabled = false;
        Camera.main.transform.position = sleepCameraAt.position;
        player.SetCameraRot(new Vector3(-4, -152, 0));

        isSleeping = true;
    }
    public void LeaveBed()
    {
        player.rb.isKinematic = false;
        player.collider.enabled = true;
        Camera.main.transform.localPosition = new Vector3(0, 0.586f, 0);

        isSleeping = false;
    }
}