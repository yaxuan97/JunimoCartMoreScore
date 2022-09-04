using StardewModdingAPI;
using StardewValley.Minigames;
using StardewValley;
using StardewModdingAPI.Events;

namespace JunimoCartMoreScore {
	public class ModConfig {
		public int Multiple { get; set; } = 1;
	}
	public class ModEntry : Mod {
		private ModConfig Config;
		public override void Entry(IModHelper helper) {
			this.Config = this.Helper.ReadConfig<ModConfig>();
			helper.Events.GameLoop.UpdateTicked += ScoreChanger;
		}
		private static int _lastscore = 0;
		private void ScoreChanger(object sender, UpdateTickedEventArgs updateTickedEventArgs){
			if(Context.IsWorldReady){
				if(Game1.currentMinigame is MineCart game){
					IReflectedField<int> rscore = Helper.Reflection.GetField<int>(game, "score");
					int score = rscore.GetValue();
					int i = score - _lastscore;
					if (i > 0) {
						_lastscore += i * Config.Multiple;
						rscore.SetValue(_lastscore);
					} else if (i == 0) { } else { 
						_lastscore = 0;
					}
				}
			}
		}
	}
}
