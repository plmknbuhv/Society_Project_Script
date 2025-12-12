using UnityEngine;
using UnityEngine.Serialization;

namespace Work.SHS.Code.MiniGame
{
    [CreateAssetMenu(fileName = "MiniGame Data", menuName = "SO/MiniGame/MiniGameData", order = 0)]
    public class MiniGameData : ScriptableObject
    {
        public string miniGameName; // 게임 이름 (도둑질)
        [TextArea(2, 4)] public string miniGameDescription; // 게임 설명
        
        public int healthCost; // 쓸 체력
        public int moneyReward; // 받을 돈
        public bool isGood;
        public string[] sucessMessage;
        public string[] failMessage;
        
        public string sceneName; // 씬 이름
    }
}