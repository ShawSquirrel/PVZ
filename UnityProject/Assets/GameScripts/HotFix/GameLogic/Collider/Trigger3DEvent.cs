using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TEngine;
using UnityEngine;
using UnityEngine.Events;

namespace GameLogic
{
    public interface IColliderEvent
    {
        public IEntity _Entity { get; set; }
    }

    public class Trigger3DEvent : MonoBehaviour, IColliderEvent
    {
        public IEntity _Entity { get; set; }

        public UnityAction<Collider> TriggerStay3DAction;
        public UnityAction<Collider> TriggerEnter3DAction;
        public UnityAction<Collider> TriggerExit3DAction;


        private void OnTriggerEnter(Collider other)
        {
            TriggerEnter3DAction?.Invoke(other);
        }


        private void OnTriggerStay(Collider other)
        {
            TriggerStay3DAction?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            TriggerExit3DAction?.Invoke(other);
        }
    }
}