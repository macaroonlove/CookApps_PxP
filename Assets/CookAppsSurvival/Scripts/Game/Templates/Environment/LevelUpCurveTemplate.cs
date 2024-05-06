using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    /// <summary>
    /// ������ ���̺�
    /// </summary>
    [CreateAssetMenu(menuName = "CookAppsSurvival/Templates/Environment/LevelUpCurve", fileName = "LevelUpCurve", order = 0)]
    public class LevelUpCurveTemplate : ScriptableObject
    {
        [Header("�ִϸ��̼� Ŀ��")]
        public AnimationCurve needExp;
    }
}