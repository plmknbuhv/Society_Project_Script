using System.Collections.Generic;
using UnityEngine;
using Work.Common.Core;

namespace Work.SHS.Code.MiniGame
{
    public class MiniGameManager : MonoSingleton<MonoBehaviour>
    {
        [field:SerializeField] public List<MiniGameData> MiniGameList { get; private set; }
    }
}