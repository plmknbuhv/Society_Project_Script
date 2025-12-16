using System;
using UnityEngine;

namespace Work.SHS.Code.MiniGame.BodyguardGame.Robbers
{
    public class RobberAnimationTrigger : MonoBehaviour
    {
        public Action OnAnimationTrigger;

        private void AnimationTrigger() => OnAnimationTrigger?.Invoke();
    }
}