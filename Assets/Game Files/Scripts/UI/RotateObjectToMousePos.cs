using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjectToMousePos : MonoBehaviour
{
    [SerializeField] GameObject pivot;
    [SerializeField] public Ray ray;
    [SerializeField] float dist = 2f;

    private void FixedUpdate()
    {
        UpdatePos();
    }

    void UpdatePos()
    {
        Vector2 mousePos = Input.mousePosition;

        ray = Camera.main.ScreenPointToRay(mousePos);
        Debug.DrawRay(ray.origin, ray.direction, Color.green);

        Vector3 lookAtPos = ray.origin + ray.direction * dist;

        pivot.transform.LookAt(lookAtPos);
    }
}
