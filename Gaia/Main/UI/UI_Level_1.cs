using System;

namespace Gaia.Main.UI
{
    internal class UI_Level_1 : IDisposable
    {
        private int score = 0;

        public UI_Element Score { get; private set; }
        public UI_Element Health { get; private set; }

        public UI_Element Dot { get; private set; }

        public UI_Level_1()
        {
            GlobalEvents.OnEarnScore += UpdateScore;

            Score = new UI_Element();
            Score.Initialize($"Score: {score}", 1, new(0, 0), PivotPoint.TopLeft);

            Health = new UI_Element();
            Health.Initialize("Health: 3", 1, new(1,0), PivotPoint.TopRight);

        }

        public void UpdateScore(int amount)
        {
            score += amount;
            Score.Content = $"Score: {score}";
        }

        public void UpdateHealth(int amount)
        {
            Health.Content = $"Health: {amount}";
        }

        // Dispose method for cleanup
        public virtual void Dispose()
        {
            GlobalEvents.OnEarnScore -= UpdateScore;
            Score?.Dispose();
            Health?.Dispose();
        }
    }
}
