using Gaia.Main.Scenes;
using System;
using System.Collections.Generic;

namespace Gaia.Utility
{
    internal static class SceneManager
    {
        public enum Scenes
        {
            Level,
            GameOver,
            MainMenu
        }

        private static readonly List<Type> sceneTypes = new()
        {
            typeof(Scene_Level_1),
            typeof(Scene_GameOver),
            typeof(Scene_MainMenu)
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
