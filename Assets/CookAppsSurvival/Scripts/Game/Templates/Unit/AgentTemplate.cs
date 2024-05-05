using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    /// <summary>
    /// �Ʊ� ���� ���ø�
    /// </summary>
    [CreateAssetMenu(menuName = "CookAppsSurvival/Templates/Agent", fileName = "Agent", order = 0)]
    public class AgentTemplate : ScriptableObject
    {
        public int id;

        [Header("�⺻ ����")]
        public string displayName;
        public EJob job;

        [Header("���ҽ� ����")]
        public Sprite face;
        public GameObject prefab;
        
        [Header("���� ����")]
        public int maxHp;
        public int ATK;
        //public int DEF;

        [Space(10)]
        public float attackTerm;
        public float attackRange;

        [Header("��ų")]
        public AgentSkillTemplate skillTemplate;
    }
}