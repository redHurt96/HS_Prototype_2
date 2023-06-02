namespace _Project.Logic.Utilities
{
    public class BushesCharacterTrigger : CharacterTrigger
    {
        private PostprocessHandler _postProcess;

        private void Start() => 
            _postProcess = FindObjectOfType<PostprocessHandler>();

        protected override void ExecuteOnEnter(PlayerMovement csPlayerController) => 
            _postProcess.EnableBushesEffect();

        protected override void ExecuteOnExit(PlayerMovement csPlayerController) => 
            _postProcess.DisableBushesEffect();
    }
}