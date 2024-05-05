using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    /// <summary>
    /// ��Ƽ ����
    /// </summary>
    [CreateAssetMenu(menuName = "CookAppsSurvival/Templates/PartySetting", fileName = "PartySetting", order = 0)]
    public class PartySettingTemplate : ScriptableObject
    {
        [Header("��Ƽ��� ")]
        public List<AgentTemplate> partyMembers = new List<AgentTemplate>();

        [Header("���� ��Ȱ ���ð�")]
        [Range(0.0f, 100.0f)]
        public float respawnTime = 5;

        [Header("��� �ִϸ��̼� ���ð�")]
        [Range(0.0f, 100.0f)]
        public float deathAnimTime = 1;
    }
}