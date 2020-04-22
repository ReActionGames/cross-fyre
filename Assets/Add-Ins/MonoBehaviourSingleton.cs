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
    private static TSelfType _instance = null;

    private static bool _autoInstantiateIfNull = false;
    private static bool _dontDestroyOnLoad = false;

    [SerializeField] private bool autoInstantiateIfNull = false;
    [SerializeField] private bool dontDestroyOnLoad = false;

    #endregion Private Fields

    #region Public Properties

    /// <summary>
    /// The single instance of this class.s
    /// </summary>
    public static TSelfType Instance
    {
        get
        {
            if (_instance != null) return _instance;
            
            _instance = (TSelfType)FindObjectOfType(typeof(TSelfType));
            if (_instance == null)
            {
                if (!_autoInstantiateIfNull)
                    return null;
                _instance = (new GameObject(typeof(TSelfType).Name)).AddComponent<TSelfType>();
            }

            if(_dontDestroyOnLoad) DontDestroyOnLoad(_instance.gameObject);
            return _instance;
        }
    }

    #endregion Public Properties

    private void Awake()
    {
        _autoInstantiateIfNull = autoInstantiateIfNull;
        _dontDestroyOnLoad = dontDestroyOnLoad;
    }
}