using Cowsins.Player;

namespace _Project.Logic.Utilities
{
    public class BushesCharacterTrigger : CharacterTrigger<PlayerStats>
    {
        protected override void ExecuteOnEnter(PlayerStats target) => 
            target.EnterBush(this);

        protected override void ExecuteOnExit(PlayerStats target) => 
            target.EscapeBush(this);
    }
}