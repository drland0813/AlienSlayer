using System;
using UnityEngine;

namespace drland.AlienSlayer
{
    public class RagdollChildPart : MonoBehaviour
    {
        public Rigidbody Rigid;
        public Collider Collider;

        private void Awake()
        {
            Rigid = GetComponent<Rigidbody>();
            Collider = GetComponent<Collider>();
        }
    }
}