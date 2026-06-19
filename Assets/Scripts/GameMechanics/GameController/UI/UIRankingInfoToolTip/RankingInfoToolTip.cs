using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankingInfoToolTip : MonoBehaviour
{
    [SerializeField] private TMP_Text textPosition;
    [SerializeField] private TMP_Text textUserName;
    [SerializeField] private TMP_Text textScore;

    public void InitializeTooltip (int position, string userName, int score)
    {
        textPosition.text = position.ToString();
        textUserName.text = userName;
        textScore.text = score.ToString();
    }
}
