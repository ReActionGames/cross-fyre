using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace CrossFyre.Core
{
    public class SceneLoaderData : SerializedScriptableObject
    {
        [TitleGroup("Scene names")] public string persistent = "PersistentScene",
            debug = "DebugScene",
            metaLevel = "MetaLevel";

        public Dictionary<Arena, string> arenaSceneNames;
        public Dictionary<OtherScene, string> otherSceneNames;
        public string sceneFolderPath = "Assets/Scenes";
    }
}