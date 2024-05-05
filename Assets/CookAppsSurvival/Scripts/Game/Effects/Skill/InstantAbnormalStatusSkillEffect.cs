using FrameWork.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    [CreateAssetMenu(menuName = "CookAppsSurvival/Effects/Skill/InstantAbnormalStatus", fileName = "Skill_InstantAbnormalStatus", order = 0)]
    public class InstantAbnormalStatusSkillEffect : SkillEffect
    {
        /// <summary>
        /// ���� ���
        /// </summary>
        [SerializeField] private EEnemyTarget abnormalTarget;

        /// <summary>
        /// ���� ����
        /// </summary>
        [EnumCondition("abnormalTarget", (int)EEnemyTarget.NumEnemyInRange, (int)EEnemyTarget.AllEnemyInRange)]
        [SerializeField] private float radius;

        /// <summary>
        /// �����̻� �ɸ��� �� ���� ��
        /// </summary>
        [EnumCondition("abnormalTarget", (int)EEnemyTarget.NumEnemyInRange)]
        [SerializeField] private int numberOfEnemies;

        /// <summary>
        /// �����̻� ����
        /// </summary>
        [SerializeField] private AbnormalStatusTemplate abnormalStatus;

        /// <summary>
        /// �����̻� ���ӽð�
        /// </summary>
        [SerializeField] private float duration;

        public override bool Excute(PartyUnit unit)
        {
            List<EnemyUnit> enemies = new List<EnemyUnit>();
            enemies.AddRange(unit.agentAttackAbility.FindAttackTargetEnemies(abnormalTarget, radius, numberOfEnemies));

            if (enemies.Count != 0)
            {
                foreach (var enemy in enemies)
                {
                    if (!enemy.healthAbility.IsAlive) continue;

                    enemy.abnormalStatusAbility.ApplyAbnormalStatus(abnormalStatus, duration);
                }
            }

            return true;
        }
    }
}
