using TMPro;
using UnityEngine;

namespace Work.SHS.Code.MiniGame.BoxMoveGame
{
    public class BoxMoveBalanceUI : MonoBehaviour
    {
        [SerializeField] private GameObject balanceIcon;
        [SerializeField] private TextMeshProUGUI distanceText;
        [SerializeField] private float moveMulti = 700f;
        
        public void MoveBalanceIcon(float moveValue)
        {
            balanceIcon.transform.localPosition = new Vector3(
                moveValue * moveMulti, balanceIcon.transform.localPosition.y, 0);
        }

        public void SetDistanceText(int distanceValue)
        {
            distanceText.text = $"{distanceValue}M";
        }
    }
}