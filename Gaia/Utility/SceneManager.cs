using Gaia.Main.Scenes;
using System;
using System.Collections.Generic;

namespace Gaia.Utility
{
    public enum Scenes
    {
        Level,
        GameOver
    }

    public static class SceneManager
    {
        private static List<Type> sceneTypes = new()
        {
            typeof(Scene_Level_1),
            typeof(Scene_GameOver)
        };

        private static Scene_Template currentScene;

        public static void LoadScene(Scenes scene)
        {
            currentScene?.Dispose();
            currentScene = CreateScene(sceneTypes[(int)scene]);
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
