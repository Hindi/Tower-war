using UnityEngine;
using System.Collections;


namespace GOF
{
    /// <summary>
    /// The class that stores options used to configure machines.
    /// </summary>
    public class GOFOption
    {
        /// <summary>The type of option used.</summary>
        public GOFactoryOptionEnum type;
        /// <summary></summary>
        public Vector3 v3;
        /// <summary></summary>
        public GameObject obj;
        /// <summary></summary>
        public float f;
        /// <summary></summary>
        public int i;
        /// <summary></summary>
        public bool b;

        /// <summary>Consructor that set the vector3</summary>
        public GOFOption(GOFactoryOptionEnum t, Vector3 v) { type = t; v3 = v; }
        /// <summary>Consructor that set the GameObject</summary>
        public GOFOption(GOFactoryOptionEnum t, GameObject o) { type = t; obj = o; }
        /// <summary>Consructor that set the float</summary>
        public GOFOption(GOFactoryOptionEnum t, float fl) { type = t; f = fl; }
        /// <summary>Consructor that set the int</summary>
        public GOFOption(GOFactoryOptionEnum t, int inte) { type = t; i = inte; }
        /// <summary>Consructor that set the int and the bool</summary>
        public GOFOption(GOFactoryOptionEnum t, int inte, bool active) { type = t; i = inte; b = active; }


        /// <summary>
        /// Use this function to add a default position option when creating or configuring a machine.
        /// </summary>
        /// <param name="v3">The position of the spawn</param>
        /// <returns>a GOFactoryOption used to configure the machine.</returns>
        public static GOFOption Position(Vector3 v3)
        {
            return new GOFOption(GOFactoryOptionEnum.position, v3);
        }

        /// <summary>
        /// Use this function to set the prefab reference when creating or configuring a machine.
        /// </summary>
        /// <param name="obj">The prefabto be spawned</param>
        /// <returns>a GOFactoryOption used to configure the machine.</returns>
        public static GOFOption Prefab(GameObject obj)
        {
            return new GOFOption(GOFactoryOptionEnum.prefab, obj);
        }

        /// <summary>
        /// Use this function to set the inactive lifespan option (the time the instances can be inactive before being destroyed) when creating or configuring a machine.
        /// </summary>
        /// <param name="f">The time</param>
        /// <returns>a GOFactoryOption used to configure the machine.</returns>
        public static GOFOption InactiveLifeSpan(float f)
        {
            return new GOFOption(GOFactoryOptionEnum.inactiveLifeSpan, f);
        }

        /// <summary>
        /// Use this function to set the lifespan option (the time the instances can be active before being recycled) when creating or configuring a machine.
        /// </summary>
        /// <param name="f">The time</param>
        /// <returns>a GOFactoryOption used to configure the machine.</returns>
        public static GOFOption LifeSpan(float f)
        {
            return new GOFOption(GOFactoryOptionEnum.lifeSpan, f);
        }

        /// <summary>
        /// Use this function to set the preinstantiate count option (the number of instance to spawn and keep inactive on Start) when creating or configuring a machine.
        /// </summary>
        /// <param name="i">The amout of instance to instantiate</param>
        /// <returns>a GOFactoryOption used to configure the machine.</returns>
        public static GOFOption PreInstantiate(int i)
        {
            return new GOFOption(GOFactoryOptionEnum.preInstantiate, i);
        }

        /// <summary>
        /// Use this function to set the network option (define if the machine instantiate on the network) when creating or configuring a machine.
        /// </summary>
        /// <param name="i">The network group</param>
        /// <param name="active">Optional parameter. Networked instantiation if true.</param>
        /// <returns>a GOFactoryOption used to configure the machine.</returns>
        public static GOFOption Network(int i, bool active = true)
        {
            return new GOFOption(GOFactoryOptionEnum.network, i, active);
        }
    }

    /// <summary>
    /// The list of possible options.
    /// </summary>
    public enum GOFactoryOptionEnum
    {
        /// <summary>Default position</summary>
        position = 0,
        /// <summary>Prefab reference</summary>
        prefab = 1,
        /// <summary>Time inactive before destruction</summary>
        inactiveLifeSpan = 2,
        /// <summary>Time active before recycling</summary>
        lifeSpan = 3,
        /// <summary>Number of instance instantiated and recycled on machine creation</summary>
        preInstantiate = 4,
        /// <summary>Set the machine on network</summary>
        network = 5
    }
}

