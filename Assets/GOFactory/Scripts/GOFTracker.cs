using UnityEngine;
using System;
using System.Collections;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GOFactoryMachine")]
namespace GOF
{
    /// <summary>
    /// This script is attached to gameobjects instanciated with the Factory.
    /// It helps keeping track of the objects to ensure reference safety.
    /// </summary>
    public class GOFTracker : MonoBehaviour
    {

        internal float lifeSpan;
        internal float inactiveLifeSpan;

        /// <summary>
        /// Unique id used by this gameobject.
        /// </summary>
        internal string id;
        /// <summary>id accessor</summary>
        public string Id
        { get { return id; } }

        /// <summary>
        /// Desactivate the gameobject and set it to recycled.
        /// </summary>
        public void recycle()
        {
            gameObject.SetActive(false);
            machine.recycle(this);
        }

        /// <summary>
        /// A reference to the machine that possesses this gameobject.
        /// </summary>
        protected GOFactoryMachine machine;
        /// <summary>machine accessor</summary>
        public GOFactoryMachine Machine
        {
            set { machine = value; }
        }

        /// <summary>
        /// If the object is destroyed, the machine must be notified before.
        /// </summary>
        void OnDestroy()
        {
            machine.forgetInstance(this);
        }
    }
}