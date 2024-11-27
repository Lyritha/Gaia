using System;
using static System.Formats.Asn1.AsnWriter;

namespace Gaia.Main.UI
{
    internal class UI_GameOver : IDisposable
    {
        public UI_Element MainText { get; private set; }


        public UI_GameOver()
        {
            MainText = new UI_Element();
            MainText.Initialize("GameOver!", 1, new(0.5f, 0.5f), PivotPoint.Center);
        }

        // Dispose method for cleanup
        public virtual void Dispose()
        {
            MainText.Dispose();
        }
    }
}
