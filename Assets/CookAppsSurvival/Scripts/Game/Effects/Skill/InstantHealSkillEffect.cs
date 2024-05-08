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
        [SerializeField] private EAgentTarget healTarget;

        /// <summary>
        /// ���� ����
        /// </summary>
        [EnumCondition("healTarget", (int)EAgentTarget.OneAgentInRange, (int)EAgentTarget.AllAgentInRange, (int)EAgentTarget.AllAgentInRangeExceptMe)]
        [SerializeField] private float radius;

        /// <summary>
        /// ȸ�� Ÿ��
        /// </summary>
        [SerializeField] private EPercentageType healType;

        /// <summary>
        /// ���ط�
        /// </summary>
        [SerializeField] private float damageAmountPer;

        /// <summary>
        /// FX
        /// </summary>
        [SerializeField] private FX fx;

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

                    fx.Play(agent, unit);
                }
            }

            return true;
        }

        public int GetAmount(PartyUnit partyUnit)
        {
            int amount;
            float typeValue = 0f;
            if (healType == EPercentageType.ATK) typeValue = partyUnit.agentAttackAbility.finalATK;
            else if (healType == EPercentageType.MaxHP) typeValue = partyUnit.healthAbility.maxHp;

            amount = (int)(typeValue * damageAmountPer);

            return amount;
        }
    }
}