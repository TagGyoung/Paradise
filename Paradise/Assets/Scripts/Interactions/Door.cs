using System.Collections;
using UnityEngine;

public class Door : Interaction
{
    [SerializeField] bool isOpen = false;

    float openAngle = 90.0f;
    float openTime = 0.5f;

    Quaternion initialRotation;
    Quaternion openRotation;

    private void Start()
    {
        initialRotation = transform.rotation;
        openRotation = Quaternion.Euler(0, initialRotation.eulerAngles.y - openAngle, 0);
    }

    public override void OnClick(RaycastHit door)
    {
        door.collider.enabled = false;

        if (isOpen) StartCoroutine(CloseDoor(door.collider));
        else StartCoroutine(OpenDoor(door.collider));

        isOpen = !isOpen;
    }

    private IEnumerator OpenDoor(Collider door)
    {
        float initialTime = 0f;

        while (initialTime < openTime)
        {
            transform.rotation = Quaternion.Slerp(initialRotation, openRotation, initialTime / openTime);

            initialTime += Time.deltaTime;

            yield return null;
        }

        transform.rotation = openRotation;

        door.enabled = true;
    }
    
    private IEnumerator CloseDoor(Collider door)
    {
        float initialTime = 0f;

        while (initialTime < openTime)
        {
            transform.rotation = Quaternion.Slerp(openRotation, initialRotation, initialTime / openTime);

            initialTime += Time.deltaTime;

            yield return null;
        }

        transform.rotation = initialRotation;

        door.enabled = true;
    }
}