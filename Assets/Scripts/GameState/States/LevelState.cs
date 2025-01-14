﻿using Signals;

namespace GameState.States
{
    public class LevelState : Base.GameState
    {
        public LevelState(GameStateMachine gameStateMachine) : base(gameStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _gameStateMachine.SignalBus.Fire<NextLevelRequest>();
        }

        public override void Exit()
        {
            base.Exit();
            _gameStateMachine.SignalBus.Fire<ExitLevelRequest>();
        }
    }
}