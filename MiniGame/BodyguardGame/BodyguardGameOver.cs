using AKH.Scripts.Players;
using AKH.Scripts.StatSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using Work.Common.Core;
using Work.Min.Code.UI;
using Work.Min.Code.UI.News;
using Work.Min.Code.UI.Transition;
using Work.SHS.Code.ETC;
using Work.SHS.Code.MiniGame.BodyguardGame.Robbers;

namespace Work.SHS.Code.MiniGame.BodyguardGame
{
    public class BodyguardGameOver : MonoBehaviour
    {
        [SerializeField] private RobberSpawner robberSpawner;
        [SerializeField] private ParticleSystem collisionParticle;
        [SerializeField] private BodyguardUI bodyguardUI;
        [SerializeField] private MiniGameData gameData;
        [SerializeField] private StatSO mulgaStat;
        [SerializeField] private NewsSO newsSO;

        private bool _isEnded;
        
        private async void OnTriggerEnter(Collider collision)
        {
            collisionParticle.transform.position = collision.transform.position;
            collisionParticle.Play();
            
            if (_isEnded) return;
            _isEnded = true;
            
            robberSpawner.StopSpawn();
            bodyguardUI.StopTimer();
            CameraManager.Instance.ImpulseCamera(1);
            
            await Awaitable.WaitForSecondsAsync(2f);
            
            newsSO.messageList.Add(gameData.failMessage[Random.Range(0, gameData.failMessage.Length)]);
            UIManager.Instance.ShowPopup("미션 실패", 0.2f, false, 
                () => GameEventBus.RaiseEvent(ChangeSceneEventChannel.ChangeSceneEvent.Init("MainMenu", 0.5f)));
        }
    }
}
