﻿using System;
using static System.Formats.Asn1.AsnWriter;

namespace Gaia.Main.UI
{
    internal class UI_GameOver : IDisposable
    {
        private UI_Element mainText;
        private UI_Element otherText;
        private UI_Element finalScore;
        private UI_Input_Listener inputListener;

        public UI_GameOver()
        {
            inputListener = new UI_Input_Listener();
            inputListener.Initialize("", 1, new(0.5f, 0.5f), PivotPoint.Center);

            mainText = new UI_Element();
            mainText.Initialize("Game Over!", 1, new(0.5f, 0.4f), PivotPoint.Center);

            finalScore = new UI_Element();
            finalScore.Initialize($"Final score: {GlobalVariables.score}", 1, new(0.5f, 0.5f), PivotPoint.Center);

            otherText = new UI_Element();
            otherText.Initialize("Right click to restart", 1, new(0.5f, 0.6f), PivotPoint.Center);
        }

        // Dispose method for cleanup
        public virtual void Dispose()
        {
            inputListener.Dispose();
            mainText.Dispose();
            finalScore.Dispose();
            otherText.Dispose();
        }
    }
}
