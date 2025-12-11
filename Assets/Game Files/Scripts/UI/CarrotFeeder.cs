using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;

public class CarrotFeeder : MonoBehaviour
{
    [SerializeField] Transform spawnParent;
    [SerializeField] RotateObjectToMousePos mouseDir;
    [SerializeField] GameObject[] button;
    [SerializeField] GameObject carrotPrefab;
    [SerializeField] Vector3 offset;
    [SerializeField] bool spawned = false;
    [SerializeField] ConfigurableJoint joint;
    [SerializeField] float fowardForceMult = 3f;

    public void SpawnCarrot()
    {
        button.SetAllActive(false);

        Instantiate(carrotPrefab,
            spawnParent.position + offset,
            Quaternion.identity,
            null);

        spawned = true;
    }

    public void GiveJoint(ConfigurableJoint _joint) => joint = _joint;

    private void Update()
    {
        ThrowCarrot();
    }

    void ThrowCarrot()
    {
        if (!spawned) return;
        if (!Input.GetMouseButtonUp(0)) return;
        button.SetAllActive(true);

        if (joint == null) return;

        Rigidbody carrotRb = joint.gameObject.Get<Rigidbody>();
        Destroy(joint);


        carrotRb.AddForce(mouseDir.ray.direction * fowardForceMult);

    }
}
