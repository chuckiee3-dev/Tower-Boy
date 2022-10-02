using System;

public static class GameActions
{
   public static Action onTimerTriggered;
   public static Action<Phase> onPhaseChanged;
   public static Action<float> onSetPhaseDuration;
   public static Action<GameState> onGameStateChanged;
   public static Action<int> onPlayerEarnXP;
   public static Action onPlayerLevelUp;

   public static void TimerTriggered()
   {
      onTimerTriggered?.Invoke();
   }

   public static void PhaseChanged(Phase currentPhase)
   {
      onPhaseChanged?.Invoke(currentPhase);
   }

   public static void SetDuration(float dur)
   {
      onSetPhaseDuration?.Invoke(dur);
   }

   public static void GameStateChanged(GameState state)
   {
      onGameStateChanged?.Invoke(state);
   }

   public static void PlayerEarnXp(int amount)
   {
      onPlayerEarnXP?.Invoke(amount);
   }

   public static void PlayerLevelUp()
   {
      onPlayerLevelUp?.Invoke();
   }

}
