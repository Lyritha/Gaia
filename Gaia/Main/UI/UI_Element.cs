using Gaia.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Gaia.Main.UI
{
    public enum PivotPoint
    {
        TopLeft,
        TopCenter,
        TopRight,
        CenterLeft,
        Center,
        CenterRight,
        BottomLeft,
        BottomCenter,
        BottomRight
    }

    internal class UI_Element : IDisposable
    {
        private string content = "Hello World";
        private int fontSize = 1;
        private Vector2 position = Vector2.Zero;
        private PivotPoint pivotPoint = PivotPoint.Center;

        private SpriteFont arialFont;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content">A string displayed</param>
        /// <param name="fontSize">Font size</param>
        /// <param name="position">Position in UV space</param>
        /// <param name="pivotPoint">Point at which this element pivots</param>
        public void Initialize(string content, int fontSize, Vector2 position, PivotPoint pivotPoint)
        {
            arialFont = GraphicsManager.Content.Load<SpriteFont>("Arial_32");

            this.content = content;
            this.fontSize = (int)(fontSize * GraphicsManager.ResolutionScaling);
            this.position =  Utils.UVToScreenPosition(position);
            this.pivotPoint = pivotPoint;

            GlobalEvents.OnDraw += DrawSelf;
            GlobalEvents.OnUpdate += Update;
        }
        
        //drawing

        private void DrawSelf(SpriteBatch pSpriteBatch)
        {
            // Calculate pivot based on the selected pivot point
            Vector2 pivot = CalculatePivot(arialFont.MeasureString(content) * fontSize);

            // Draw the string with the calculated pivot
            pSpriteBatch.DrawString(
                arialFont,
                content,
                position,
                Color.White,
                0,
                pivot,
                fontSize,
                SpriteEffects.None,
                0
            );
        }

        public virtual void Update(float deltaTime)
        {
        }

        private Vector2 CalculatePivot(Vector2 textSize)
        {
            return pivotPoint switch
            {
                PivotPoint.TopLeft => Vector2.Zero,
                PivotPoint.TopCenter => new(textSize.X / 2, 0),
                PivotPoint.TopRight => new(textSize.X, 0),
                PivotPoint.CenterLeft => new(0, textSize.Y / 2),
                PivotPoint.Center => textSize / 2,
                PivotPoint.CenterRight => new(textSize.X, textSize.Y / 2),
                PivotPoint.BottomLeft => new(0, textSize.Y),
                PivotPoint.BottomCenter => new(textSize.X / 2, textSize.Y),
                PivotPoint.BottomRight => textSize,
                _ => Vector2.Zero, // Default fallback
            };
        }

        //utility
        
        public int FontSize
        {
            get => fontSize;
            set => fontSize = (int)(value * GraphicsManager.ResolutionScaling);
        }

        public string Content
        {
            get => content;
            set => content = value;
        }

        /// <summary>
        /// Position in uv space
        /// </summary>
        public Vector2 Position
        {
            get => position;
            set => Utils.UVToScreenPosition(value);
        }

        public PivotPoint PivotPoint
        {
            get => pivotPoint;
            set => pivotPoint = value;
        }

        // Dispose method for cleanup
        public virtual void Dispose()
        {
            GlobalEvents.OnDraw -= DrawSelf;
            GlobalEvents.OnUpdate -= Update;
            arialFont = null;  // Set to null so the reference is cleared
        }
    }
}

