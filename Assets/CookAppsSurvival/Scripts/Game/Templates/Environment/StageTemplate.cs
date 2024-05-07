using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    [CreateAssetMenu(menuName = "CookAppsSurvival/Templates/Environment/Stage", fileName = "Stage", order = 0)]
    public class StageTemplate : ScriptableObject
    {
        [Header("��")]
        public string sceneName;

        [Header("�� ����")]
        public EnemyTemplate normalEnemy;
        public EnemyTemplate bossEnemy;

        [Header("�������� óġ�ؾ� �� ���� ��")]
        public int killUntilBoss;

        [Header("��Ƽ ����")]
        public PartySettingTemplate partySettingTemplate;

        [Header("���� ����")]
        public EnemySpawnTemplate spawnTemplate;
    }
}