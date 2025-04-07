using UnityEngine;

namespace USPDigital
{
    /// <summary>
    /// Generic singleton base class for MonoBehaviours.
    /// Ensures only one instance of the component exists in the scene.
    /// </summary>
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        /// <summary>
        /// Indicates whether this instance was destroyed on Awake due to duplication.
        /// </summary>
        public bool DestroyOnAwake { get; private set; }

        /// <summary>
        /// Gets the singleton instance of this MonoBehaviour.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    // Find the instance in the scene if it hasn't been assigned yet
                    instance = (T)FindObjectOfType(typeof(T));
                    if (instance == null)
                    {
                        Debug.Log(typeof(T) + " is not Awake");
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Validates the singleton instance.
        /// </summary>
        protected virtual void Awake()
        {
            CheckInstance();
        }

        // Ensures only one instance exists; destroys duplicate GameObjects
        protected bool CheckInstance()
        {
            if (this == Instance)
            {
                return true;
            }

            // Destroy the duplicate instance and its GameObject
            Destroy(this);
            Destroy(gameObject);
            DestroyOnAwake = true;
            return false;
        }

        /// <summary>
        ///Clears instance reference if not a duplicate.
        /// </summary>
        protected virtual void OnDestroy()
        {
            if (!DestroyOnAwake)
            {
                instance = null;
            }
        }
    }
}
