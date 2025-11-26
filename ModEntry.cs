using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace SpaDailyReward
{
    public class ModEntry : Mod
    {
        private ModConfig Config;
        private int rewardAmount;
        private bool dailyReward = false;

        public override void Entry(IModHelper helper)
        {
            this.Config = this.Helper.ReadConfig<ModConfig>();
            this.rewardAmount = this.Config.RewardAmount;

            helper.Events.Player.Warped += this.OnPlayerWarp;
            helper.Events.GameLoop.TimeChanged += this.OnTimeChanged;
        }

        private void OnPlayerWarp(object sender, WarpedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
                return;

            if (e.NewLocation.Name == "BathHouse_Entry" && !dailyReward)
            {
                Game1.player.addUnearnedMoney(rewardAmount);
                dailyReward = true;
            }
        }

         private void OnTimeChanged(object sender, TimeChangedEventArgs e)
         {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
                return;

            if (e.NewTime == 600)
            {
                dailyReward = false;
            }
        }
    }
}

