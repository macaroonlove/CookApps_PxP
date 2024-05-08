using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    [CreateAssetMenu(menuName = "CookAppsSurvival/Templates/State/AbnormalStatus", fileName = "AbnormalStatus", order = 0)]
    public class AbnormalStatusTemplate : ScriptableObject
    {
        public string displayName;

        [TextArea]
        public string description;

        public FX fx;

        [Header("ȿ�� ����")]
        public List<Effect> effects;
    }
}
