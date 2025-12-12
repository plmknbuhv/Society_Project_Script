using System.Collections.Generic;
using AKH.Scripts.Players;
using AKH.Scripts.StatSystem;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Work.Common.Core;
using Work.Min.Code.EventChannel;
using Work.Min.Code.UI;
using Work.Min.Code.UI.News;
using Work.SHS.Code.MiniGame.BodyguardGame.Robbers;

namespace Work.SHS.Code.MiniGame.BodyguardGame
{
    public class BodyguardUI : MonoBehaviour
    {
        [SerializeField] private float gameTime = 30f;
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private RobberSpawner robberSpawner;
        [SerializeField] private List<Image> keyBorderImages;
        [SerializeField] private BodyguardInputSO inputSO;
        [SerializeField] private NewsSO newsSO;
        [SerializeField] private MiniGameData gameData;
        [SerializeField] private StatSO mulgaStat;
        
        private List<Material> _materials = new List<Material>();
        private bool _isTimerActive;
        private float _timer;

        private readonly int _isSelectedHash = Shader.PropertyToID("_IsSelected");

        private void Awake()
        {
            foreach (var borderImage in keyBorderImages)
            {
                Material mat = new Material(borderImage.material);
                borderImage.material = mat;
                _materials.Add(mat);
            }
            
            GameEventBus.AddListener<StartGameEvent>(HandleStartGame);
        }

        private void OnDestroy()
        {
            GameEventBus.RemoveListener<StartGameEvent>(HandleStartGame);
        }

        public void HandleStartGame(StartGameEvent evt)
        {
            _isTimerActive = true;
        }
        
        private void Update()
        {
            for (int i = 0; i < 4; i++)
                _materials[i].SetInt(_isSelectedHash, (int)inputSO.InputType-1 == i ? 1 : 0);
            
            if (_isTimerActive)
            {
                _timer += Time.deltaTime;
                timerText.text = $"{(_timer < gameTime ? _timer : gameTime):F1}";
            }
            
            if (_timer >= gameTime)
            {
                robberSpawner.StopSpawn();

                if (_timer > gameTime + 5f)
                {
                    _isTimerActive = false;
                    _timer = 0f;
                    
                    newsSO.messageList.Add(gameData.sucessMessage[Random.Range(0, gameData.sucessMessage.Length)]);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    UIManager.Instance.ShowPopup("미션 성공", 0.2f, false, 
                        () => SceneManager.LoadScene(1));
                    CurrencyManager.Instance.ModifyStat(StatType.Money, Mathf.FloorToInt(gameData.moneyReward * PlayerStat.Instance.GetBaseValue(mulgaStat)));
                }
            }
        }
        
        public void StopTimer() => _isTimerActive = false;
    }
}
