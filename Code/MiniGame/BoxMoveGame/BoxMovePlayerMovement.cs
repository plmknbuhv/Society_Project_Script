using AKH.Scripts.Entities;
using AKH.Scripts.Players;
using AKH.Scripts.StatSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Work.Common.Core;
using Work.Min.Code.UI;
using Work.Min.Code.UI.News;
using Work.Min.Code.UI.Transition;
using Work.SHS.Code.ETC;

namespace Work.SHS.Code.MiniGame.BoxMoveGame
{
    public class BoxMovePlayerMovement : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float idleMultiSpeed = 1.2f;
        [SerializeField] private float defaultIdleSpeed = 0.001f;
        [SerializeField] private BoxMoveBalanceUI balanceUI;
        [SerializeField] private MiniGameData gameData;
        [SerializeField] private StatSO mulgaStat;
        [SerializeField] private NewsSO newsSO;
        
        private BoxMovePlayer _player;
        private BoxMovePlayerAnimator _animator;
        private BoxMoveInputSO _inputSO;
        private BoxPlayerMoveDistanceTracker _distanceTracker;

        private ChangeSceneEvent _changeSceneEvt = ChangeSceneEventChannel.ChangeSceneEvent;
        private readonly string _mainScene = "MainMenu";
        
        private float _xMoveValue;
        private bool _isStarted;

        public UnityEvent OnDeadEvent;
        
        public void Initialize(Entity entity)
        {
            _player = entity as BoxMovePlayer;
            _inputSO = _player.InputSO;
            _distanceTracker = _player.GetCompo<BoxPlayerMoveDistanceTracker>();
            _animator = _player.GetCompo<BoxMovePlayerAnimator>();
        }
        
        public void HandleStart()
        {
            _xMoveValue = Random.value > 0.5f ? -0.005f : 0.005f;
            _isStarted = true;
            _animator.ApplyRootMotion = true;
        }

        private void StopGame()
        {
            _isStarted = false;
            _animator.ApplyRootMotion = false;
            _animator.SetMoveParam(new Vector2(_xMoveValue, 0), false);
        }

        private void Update()
        {
            if (_isStarted == false)
            {
                _animator.SetMoveParam(new Vector2(0, 0), true);
                return;
            }
            
            Move();
        }

        private void Move()
        {
            if (_inputSO.moveDir.x != 0)
            {
                _xMoveValue += _inputSO.moveDir.x * (moveSpeed * Time.deltaTime);
                _xMoveValue = Mathf.Clamp(_xMoveValue, -1, 1);
            }
            
            _xMoveValue += _xMoveValue * (idleMultiSpeed * Time.deltaTime) 
                           + Mathf.Sign(_xMoveValue) * defaultIdleSpeed * Time.deltaTime;
            _xMoveValue = Mathf.Clamp(_xMoveValue, -1, 1);
            
            _animator.SetMoveParam(new Vector2(_xMoveValue, 1), false);
            balanceUI.MoveBalanceIcon(_xMoveValue);

            if (Mathf.Approximately(Mathf.Abs(_xMoveValue), 1))
            {
                StopGame();
                OnDeadEvent?.Invoke();
                CameraManager.Instance.ImpulseCamera(0.65f);
                ShowResultPopup(false);
            }

            if (_distanceTracker.MoveDistance > 50f)
            {
                StopGame();
                ShowResultPopup(true);
            }
        }

        private async void ShowResultPopup(bool isSuccess)
        {
            await Awaitable.WaitForSecondsAsync(isSuccess ? 1.3f : 2.5f);

            if (isSuccess)
            {
                CurrencyManager.Instance.ModifyStat(StatType.Money, Mathf.FloorToInt(gameData.moneyReward * 
                    PlayerStat.Instance.GetBaseValue(mulgaStat)));
                newsSO.messageList.Add(gameData.sucessMessage[Random.Range(0, gameData.sucessMessage.Length)]);
            }
            else
                newsSO.messageList.Add(gameData.failMessage[Random.Range(0, gameData.failMessage.Length)]);
            
            UIManager.Instance.ShowPopup(isSuccess ? "미션 성공" : "미션 실패", 0.2f, false, 
                () => GameEventBus.RaiseEvent(_changeSceneEvt.Init(_mainScene, 0.5f)));
        }
    }
}