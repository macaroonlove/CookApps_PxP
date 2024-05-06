using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    /// <summary>
    /// �� ���� ���ø�
    /// </summary>
    [CreateAssetMenu(menuName = "CookAppsSurvival/Templates/Enemy", fileName = "Enemy", order = 0)]
    public class EnemyTemplate : ScriptableObject
    {
        public int id;

        [Header("�⺻ ����")]
        public string displayName;

        public EEnemyType enemyType;
        
        [Space(10)]
        public GameObject prefab;

        [Header("���� ����")]
        public int maxHp;
        public int ATK;
        //public int DEF;

        [Space(10)]
        public float attackTerm;
        public float attackRange;

        [Header("�̵� ����")]
        public float moveSpeed;
        public float trackableDistance;

        [Header("���� ����")]
        public float patrolRadius;
        public float patrolWaitTime;
    }
}