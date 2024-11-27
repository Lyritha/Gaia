using System;
using static System.Formats.Asn1.AsnWriter;

namespace Gaia.Main.UI
{
    internal class UI_MainMenu : IDisposable
    {
        private UI_Element mainText;
        private UI_Element otherText;
        private UI_Input_Listener inputListener;

        public UI_MainMenu()
        {
            inputListener = new UI_Input_Listener();
            inputListener.Initialize("", 1, new(0.5f, 0.5f), PivotPoint.Center);

            mainText = new UI_Element();
            mainText.Initialize("Gaia", 1, new(0.5f, 0.45f), PivotPoint.Center);

            otherText = new UI_Element();
            otherText.Initialize("Right click to start", 1, new(0.5f, 0.55f), PivotPoint.Center);
        }

        // Dispose method for cleanup
        public virtual void Dispose()
        {
            inputListener.Dispose();
            mainText.Dispose();
            otherText.Dispose();
        }
    }
}
