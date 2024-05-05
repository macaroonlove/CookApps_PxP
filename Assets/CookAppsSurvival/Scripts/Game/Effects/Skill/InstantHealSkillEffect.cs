using FrameWork.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    [CreateAssetMenu(menuName = "CookAppsSurvival/Effects/Skill/InstantHeal", fileName = "Skill_InstantHeal", order = 0)]
    public class InstantHealSkillEffect : SkillEffect
    {
        /// <summary>
        /// ���� ���
        /// </summary>
        public EAgentTarget healTarget;

        /// <summary>
        /// ���� ����
        /// </summary>
        [EnumCondition("healTarget", (int)EAgentTarget.OneAgentInRange, (int)EAgentTarget.AllAgentInRange, (int)EAgentTarget.AllAgentInRangeExceptMe)]
        public float radius;

        /// <summary>
        /// ȸ�� Ÿ��
        /// </summary>
        public EPercentageType healType;

        /// <summary>
        /// ���ط�
        /// </summary>
        public float damageAmountPer;

        public override bool Excute(PartyUnit unit)
        {
            List<PartyUnit> agents = new List<PartyUnit>();
            agents.AddRange(unit.agentAttackAbility.FindHealTargetMembers(healTarget, radius));

            if (agents.Count >= 0)
            {
                foreach (var agent in agents)
                {
                    if (!agent.healthAbility.IsAlive) continue;

                    var healAmount = GetAmount(unit);

                    agent.healthAbility.Healed(healAmount);
                }
            }

            return true;
        }

        public int GetAmount(PartyUnit partyUnit)
        {
            int amount;
            float typeValue = 0f;
            if (healType == EPercentageType.ATK) typeValue = partyUnit.pureATK;
            else if (healType == EPercentageType.MaxHP) typeValue = partyUnit.healthAbility.maxHp;

            amount = (int)(typeValue * damageAmountPer);

            return amount;
        }
    }
}
