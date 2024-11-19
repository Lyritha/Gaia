using System;

namespace Gaia.Main.UI
{
    internal class UI_Template : IDisposable
    {
        public UI_Element Score { get; private set; }
        public UI_Element Health { get; private set; }

        public UI_Element Dot { get; private set; }

        public  UI_Template()
        {
            Score = new UI_Element();
            Score.Initialize("100", 1, new(0, 0), PivotPoint.TopLeft);

            Health = new UI_Element();
            Health.Initialize("3", 1, new(1,0), PivotPoint.TopRight);

            Dot = new UI_Element();
            Dot.Initialize(".", 1, new(0.5f, 0.5f), PivotPoint.Center);
        }

        // Dispose method for cleanup
        public virtual void Dispose()
        {
            Score?.Dispose();
            Health?.Dispose();
        }
    }
}
