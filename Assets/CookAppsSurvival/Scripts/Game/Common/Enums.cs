using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    public enum EJob
    {
        Tanker,
        Melee,
        Archer,
        Priest,
    }

    public enum EEnemyType
    {
        Normal,
        Boss,
    }

    public enum EPercentageType
    {
        /// <summary>
        /// ���ݷ��� n%
        /// </summary>
        ATK,
        /// <summary>
        /// �ִ�ü���� n%
        /// </summary>
        MaxHP,
    }

    public enum EEnemyTarget
    {
        /// <summary>
        /// ���� ����
        /// </summary>
        ExistingEnemy,
        /// <summary>
        /// ���� �� ���� �ϳ�
        /// </summary>
        OneEnemyInRange,
        /// <summary>
        /// ���� �� ���� (��)��ŭ
        /// </summary>
        NumEnemyInRange,
        /// <summary>
        /// ���� �� ���� ���
        /// </summary>
        AllEnemyInRange,
        /// <summary>
        /// ��� ����
        /// </summary>
        AllEnemy,
    }

    public enum EAgentTarget
    {
        /// <summary>
        /// �ڱ� �ڽ�
        /// </summary>
        Myself,
        /// <summary>
        /// ���� �� �Ʊ� �ϳ�
        /// </summary>
        OneAgentInRange,
        /// <summary>
        /// ���� �� �Ʊ� ���
        /// </summary>
        AllAgentInRange,
        /// <summary>
        /// �ڽ��� ������ ���� �� �Ʊ� ���
        /// </summary>
        AllAgentInRangeExceptMe,
    }
}
