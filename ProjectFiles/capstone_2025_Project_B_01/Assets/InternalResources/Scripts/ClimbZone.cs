using UnityEngine;

public class ClimbZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (PlayerController.IsPlayer(other)) PlayerController.instance.SetClimbing(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (PlayerController.IsPlayer(other)) PlayerController.instance.SetClimbing(false);
    }
}
