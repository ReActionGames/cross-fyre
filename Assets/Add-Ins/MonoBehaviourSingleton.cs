using UnityEngine;

/// <summary>
/// A singleton base class for easier singleton implementation.
/// </summary>
/// <typeparam name="TSelfType">The type of the class inheriting this class.</typeparam>
public class MonoBehaviourSingleton<TSelfType> : MonoBehaviour where TSelfType : MonoBehaviour
{
    #region Private Fields

    /// <summary>
    /// The single instance of this class.
    /// </summary>
    private static TSelfType m_Instance = null;

    private static bool m_autoInstantiateIfNull = false;

    [SerializeField] private bool autoInstantiateIfNull = false;

    #endregion Private Fields

    #region Public Properties

    /// <summary>
    /// The single instance of this class.s
    /// </summary>
    public static TSelfType Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = (TSelfType)FindObjectOfType(typeof(TSelfType));
                if (m_Instance == null)
                {
                    if (!m_autoInstantiateIfNull)
                        return null;
                    m_Instance = (new GameObject(typeof(TSelfType).Name)).AddComponent<TSelfType>();
                }

                DontDestroyOnLoad(m_Instance.gameObject);
            }
            return m_Instance;
        }
    }

    #endregion Public Properties

    private void Awake()
    {
        m_autoInstantiateIfNull = autoInstantiateIfNull;
    }
}