using UnityEngine;

namespace PizzaMaker.Tools
{
    /// <summary>
    /// A helper class to handle singleton pattern, but only in one scene.
    /// The instance will be destroyed when the scene changed.
    /// </summary>
    /// <example>
    /// <code>
    /// <![CDATA[
    /// public class ItemSpawner : SceneSingleton<ItemSpawner>
    /// {
    ///     protected override void Awake()
    ///     {
    ///         base.Awake();   //important!
    ///         ...             //do the rest here
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// </example>
    public class SceneSingleton<T> : MonoBehaviour where T : Component
    {
        private static T instance;

        public static T Instance
        {
            get
            {
#if UNITY_EDITOR
                if (instance == null)
                {
                    //Debug.LogError("Scene singleton instance does not exist.");
                    return null;
                }
#endif
                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("Only one scene singleton instance can exist.");
                return;
            }

            instance = this as T;
        }

        protected virtual void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }
    }
}
