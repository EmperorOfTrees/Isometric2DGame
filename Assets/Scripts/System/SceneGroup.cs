using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Eflatun.SceneReference;

namespace Systems.SceneManagement
{
    [Serializable]
    public class SceneGroup
    {
        public string GroupName = "New Scene Group";
        public List<SceneData> Scenes;

        public string FindSceneNameByType(SceneType scenetype)
        {
            return Scenes.FirstOrDefault(scene => scene.SceneType == scenetype)?.SceneReference.Name;
        }
    }
    [Serializable]
    public class SceneData
    {
        public SceneReference SceneReference;
        public string Name => SceneReference.Name;
        public SceneType SceneType;
    }
    public enum SceneType
    {
        ActiveScene,
        MainMenu,
        UserInterface,
        HUD,
        Cinematic,
        Environment, 
        Tooling
    }
}



