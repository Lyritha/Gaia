using Gaia.Main.Scenes;
using System;
using System.Collections.Generic;

namespace Gaia.Utility
{
    public static class SceneManager
    {
        private static List<Type> sceneTypes = new();
        private static Scene_Template currentScene;

        static SceneManager()
        {
            sceneTypes.Add(typeof(Scene_Level_1));
        }

        public static void LoadScene(int sceneIndex = 0)
        {
            currentScene?.Dispose();
            currentScene = CreateScene(sceneTypes[sceneIndex]);
            currentScene.LoadScene();
        }

        public static void ReloadScene()
        {
            currentScene?.Dispose();
            currentScene = CreateScene(currentScene.GetType());
            currentScene.LoadScene();
        }

        private static Scene_Template CreateScene(Type sceneType)
        {
            if (!typeof(Scene_Template).IsAssignableFrom(sceneType))
                throw new InvalidOperationException($"Type {sceneType.Name} is not a Scene.");

            return (Scene_Template)Activator.CreateInstance(sceneType);
        }
    }
}
