using System.Collections;

namespace CrossFyre
{
    public class CoroutineHelper : MonoBehaviourSingleton<CoroutineHelper>
    {
        public static void RunCoroutine(IEnumerator coroutine)
        {
            Instance.StartCoroutine(coroutine);
        }

        public static void HaltCoroutine(IEnumerator coroutine)
        {
            Instance.StopCoroutine(coroutine);
        }
    }
}
