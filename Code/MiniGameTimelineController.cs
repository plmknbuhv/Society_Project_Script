using UnityEngine;
using UnityEngine.Playables;
using Work.Common.Core;
using Work.Min.Code.EventChannel;

namespace Work.SHS.Code
{
    public class MiniGameTimelineController : MonoBehaviour
    {
        [SerializeField] private PlayableDirector playableDirector;

        private void Start()
        {
            GameEventBus.AddListener<EndTutorialEvent>(HandleEndTutorialEvent);
        }

        private void OnDestroy()
        {
            GameEventBus.RemoveListener<EndTutorialEvent>(HandleEndTutorialEvent);
        }

        private async void HandleEndTutorialEvent(EndTutorialEvent evt)
        {
            playableDirector.Play();
            await Awaitable.WaitForSecondsAsync((float)playableDirector.duration);
            GameEventBus.RaiseEvent(TutorialEventChannel.StartTimerEvent);
        }
    }
}