using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    [CreateAssetMenu(menuName = "CookAppsSurvival/Templates/State/Buff", fileName = "Buff", order = 0)]
    public class BuffTemplate : ScriptableObject
    {
        public string displayName;

        public int duration;

        [Header("ȿ�� ����")]
        public List<Effect> effects;
    }
}
