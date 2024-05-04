using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    /// <summary>
    /// ��Ƽ�� ��Ȱ�� �ʿ��� ��� ���ø�
    /// </summary>
    [CreateAssetMenu(menuName = "CookAppsSurvival/Templates/PartyRevival", fileName = "PartyRevival", order = 0)]
    public class PartyRevivalTemplate : ScriptableObject
    {
        [Header("��Ȱ �ð�")]
        [Range(0.0f, 100.0f)]
        public float respawnTime = 5;
    }
}