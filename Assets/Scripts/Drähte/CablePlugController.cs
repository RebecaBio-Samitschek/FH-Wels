using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CablePlugController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        CableDrag cableDrag = other.gameObject.GetComponent<CableDrag>();

        if (cableDrag != null)
        {
            if (cableDrag.IsDragging)
            {
                cableDrag.StayInRotatedState();

            }
            else
            {
                cableDrag.StopDragging();
            }
        }
    }

}
 