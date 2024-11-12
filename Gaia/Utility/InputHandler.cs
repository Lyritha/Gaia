using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Gaia.Utility
{
    public static class InputHandler
    {
        // Static fields to store input state
        public static Vector2 Wasd { get; private set; } = Vector2.Zero;
        public static Vector2 MousePos { get; private set; } = Vector2.Zero;

        public static bool IsMouseLeftDown { get; private set; } = false;
        public static bool IsMouseRightDown { get; private set; } = false;

        // Update method to be called each frame
        public static void Update(float deltaTime)
        {
            GetKeyboardInput();
            GetMouseInput();
        }

        // Private method to get keyboard input
        private static void GetKeyboardInput()
        {
            Vector2 keyboardInput = Vector2.Zero;
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.W)) keyboardInput.Y += 1;
            if (keyboardState.IsKeyDown(Keys.S)) keyboardInput.Y -= 1;
            if (keyboardState.IsKeyDown(Keys.D)) keyboardInput.X += 1;
            if (keyboardState.IsKeyDown(Keys.A)) keyboardInput.X -= 1;

            if (keyboardInput != Vector2.Zero)
                keyboardInput.Normalize();

            Wasd = keyboardInput;
        }

        // Private method to get mouse input
        private static void GetMouseInput()
        {
            MouseState mouseState = Mouse.GetState();

            IsMouseLeftDown = mouseState.LeftButton == ButtonState.Pressed;
            IsMouseRightDown = mouseState.RightButton == ButtonState.Pressed;

            MousePos = mouseState.Position.ToVector2();
        }
    }
}
