using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : Interaction
{
    [SerializeField] GameObject LeftDrawer;
    [SerializeField] GameObject RightDrawer;
    [SerializeField] AudioClip lockAudioClip;
    [SerializeField] AudioClip collectionAudioClip;

    GameObject leftRope;
    GameObject rightRope;

    void Start()
    {
        lockAudioClip = Resources.Load<AudioClip>("Wood Lock");
        collectionAudioClip = Resources.Load<AudioClip>("Cut Rope");

        leftRope = LeftDrawer.transform.GetChild(0).gameObject;
        rightRope = RightDrawer.transform.GetChild(0).gameObject;
    }

    string item = "Knife";

    public override void OnClick(Collider door)
    {
        if (Inventory.Instance.UseItem(item))
        {
            AudioManager.Instance.Sound(collectionAudioClip);

            Destroy(LeftDrawer.GetComponent<Animator>());
            Destroy(RightDrawer.GetComponent<Animator>());

            leftRope.SetActive(true);
            rightRope.SetActive(true);
            StartCoroutine(IECutRope());
        }
        else
        {
            AudioManager.Instance.Sound(lockAudioClip);

            LeftDrawer.GetComponent<Animator>().Play("Left Cabinet Door");
            RightDrawer.GetComponent<Animator>().Play("Right Cabinet Door");
        }
    }

    IEnumerator IECutRope()
    {
        float speed = 0.02f;

        leftRope.SetActive(true);
        rightRope.SetActive(true);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);

        LeftDrawer.layer = 8;
        RightDrawer.layer = 8;

        while (leftRope.transform.localRotation.z >= -0.707)
        {
            leftRope.transform.localRotation = Quaternion.Lerp
                (
                    leftRope.transform.localRotation,
                    Quaternion.Euler(leftRope.transform.localRotation.x, leftRope.transform.localRotation.y, -90),
                    speed
                );

            rightRope.transform.localRotation = Quaternion.Lerp
                (
                    rightRope.transform.localRotation,
                    Quaternion.Euler(rightRope.transform.localRotation.x, rightRope.transform.localRotation.y, 90),
                    speed
                );

            yield return null;
        }

        Destroy(gameObject);
    }
}