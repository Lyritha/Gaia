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
            Score.Initialize("Score: 0", 1, new(0, 0), PivotPoint.TopLeft);

            Health = new UI_Element();
            Health.Initialize("Health: 3", 1, new(1,0), PivotPoint.TopRight);

        }

        public void UpdateScore(int amount)
        {
            Score.Content = $"Score: {amount}";
        }

        public void UpdateHealth(int amount)
        {
            Health.Content = $"Health: {amount}";
        }

        // Dispose method for cleanup
        public virtual void Dispose()
        {
            Score?.Dispose();
            Health?.Dispose();
        }
    }
}
