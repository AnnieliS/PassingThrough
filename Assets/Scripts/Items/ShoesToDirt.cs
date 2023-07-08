using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoesToDirt : MonoBehaviour
{
   [SerializeField] Collider2D dirtCollider;

   private void OnDestroy() {
    dirtCollider.enabled = true;
   }
}
