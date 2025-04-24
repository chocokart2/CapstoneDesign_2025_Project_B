using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 offset;
    [SerializeField] float smoothness;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = transform.position - PlayerController.instance.transform.position;
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        Vector3 destinationPosition = PlayerController.instance.transform.position + offset;
        transform.position = Vector3.Lerp(destinationPosition, transform.position, smoothness);
    }
}
