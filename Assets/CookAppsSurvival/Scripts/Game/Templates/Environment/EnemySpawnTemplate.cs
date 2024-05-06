using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    /// <summary>
    /// ���͸� �����ϴµ� �ʿ��� ��� ���ø�
    /// </summary>
    [CreateAssetMenu(menuName = "CookAppsSurvival/Templates/Environment/EnemySpawn", fileName = "EnemySpawn", order = 0)]
    public class EnemySpawnTemplate : ScriptableObject
    {
        [Header("���� �ֱ�")]
        [Range(0.0f, 100.0f)]
        public float spawnTime = 5;
        
        [Header("���� ���� ������ �ִ� ��")]
        [Range(1, 100)]
        public int simultaneousMaxCnt = 5;

        [Header("���Ͱ� ��Ÿ�� ��ġ(�÷��̾� + ������)")]
        [Range(1.0f, 200.0f)]
        public float spawnRadius = 1;
        
        [Header("���� Ǯ�� ������")]
        [Range(1, 500)]
        public int poolSize = 10;
    }
}