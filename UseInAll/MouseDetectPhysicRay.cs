using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDetectPhysicRay : MonoBehaviour
{
    protected RaycastHit hit;
    protected Ray ray;

    protected Vector3 mousePosition;

    public Transform GetMouseHitTranform()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            MouseHitObject();
            Debug.Log(hit.transform.name, hit.transform.gameObject);
            return hit.transform;
        }
        return null;
    }

    public bool MouseHitObjectCheck(Transform objectHit)
    {
        if (objectHit == GetMouseHitTranform())
        {
            return true;
        }
        return false;
    }

    protected virtual void MouseHitObject()
    {
        mousePosition = hit.point;
    }

    public Vector3 GetMousePosion()
    {
        return mousePosition;
    }
}
