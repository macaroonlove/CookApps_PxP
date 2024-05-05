using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    [CreateAssetMenu(menuName = "CookAppsSurvival/Templates/AgentSkill", fileName = "Skill", order = 0)]
    public class AgentSkillTemplate : ScriptableObject
    {
        [Header("�⺻����")]
        public string displayName;
        [TextArea]
        public string description;

        [Space(10)]
        public float cooldownTime;

        [Header("���ҽ� ����")]
        public Sprite face;        

        [Header("��ų ����")]
        public List<SkillEffect> effects;
    }
}
