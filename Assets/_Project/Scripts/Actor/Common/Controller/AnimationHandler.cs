using Zenject;

namespace PlayerBehaviors
{
    public class AnimationHandler : IInitializable
    {
        #region INJECT

        private readonly BaseModel _baseModel;

        private AnimationHandler(BaseModel baseModel) => _baseModel = baseModel;

        #endregion

        public void Initialize()
        {
            _baseModel.GetAnimator.keepAnimatorControllerStateOnDisable = false;
        }

        public void Play(int anim) =>
            _baseModel.GetAnimator.Play(anim);

        public void CrossFadeInFixed(int anim, int layer, float transitionDuration) =>
            _baseModel.GetAnimator.CrossFadeInFixedTime(anim, transitionDuration, layer, 0, 0);
    }
}