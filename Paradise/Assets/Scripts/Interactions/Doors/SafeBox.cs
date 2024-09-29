using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeBox : Door
{
    [SerializeField] Transform safeDoor;
    [SerializeField] GameObject cabinetDoor;
    [SerializeField] GameObject key;

    private void Start()
    {
        initialRotation = safeDoor.rotation;

        openAngle = 95.0f;
        openTime = 0.6f;

        openDoorAudio = AudioManager.Instance.GetAudioClip("SafeBox");

        openRotation = Quaternion.Euler(initialRotation.eulerAngles.x, initialRotation.eulerAngles.y + openAngle, initialRotation.eulerAngles.z);
    }

    public override void OnClick(Collider safe)
    {
        safe.enabled = false;

        if (isOpen) StartCoroutine(CloseDoor(safe));
        else StartCoroutine(OpenDoor(safe));

        AudioManager.Instance.Sound(openDoorAudio);

        isOpen = !isOpen;
    }

    new private IEnumerator OpenDoor(Collider safe)
    {
        float initialTime = 0f;
    
        cabinetDoor.layer = 0;
    
        while (initialTime < openTime)
        {
            safeDoor.transform.rotation = Quaternion.Slerp(initialRotation, openRotation, initialTime / openTime);
    
            initialTime += Time.deltaTime;
    
            yield return null;
        }
    
        safeDoor.rotation = openRotation;
    
        safe.enabled = true;

        key.layer = 8;
    }
    
    new private IEnumerator CloseDoor(Collider safe)
    {
        key.layer = 0;

        float initialTime = 0f;
    
        cabinetDoor.layer = 8;
    
        while (initialTime < openTime)
        {
            safeDoor.transform.rotation = Quaternion.Slerp(openRotation, initialRotation, initialTime / openTime);
    
            initialTime += Time.deltaTime;
    
            yield return null;
        }
    
        safeDoor.rotation = initialRotation;
    
        safe.enabled = true;
    }
}