using FrameWork.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        [SerializeField] private float radius;

        /// <summary>
        /// ȸ�� Ÿ��
        /// </summary>
        [SerializeField] private EPercentageType healType;

        /// <summary>
        /// ȸ����
        /// </summary>
        [SerializeField] private float damageAmountPer;

        /// <summary>
        /// FX
        /// </summary>
        [SerializeField] private FX fx;

        public override List<Unit> GetTarget(PartyUnit unit)
        {
            List<Unit> agents = new List<Unit>();
            agents.AddRange(unit.agentAttackAbility.FindHealTargetMembers(healTarget, radius));

            return agents;
        }

        public override void Excute(PartyUnit unit, Unit agent)
        {
            if (unit == null || agent == null) return;
            if (!agent.healthAbility.IsAlive) return;

            var healAmount = GetAmount(unit);

            agent.healthAbility.Healed(healAmount);

            if (fx != null)
            {
                fx.Play(agent, unit);
            }
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

        public override string GetLabel()
        {
            return "��� ȸ�� ��ų";
        }

#if UNITY_EDITOR
        public override void Draw(Rect rect)
        {
            var labelRect = new Rect(rect.x, rect.y, 140, rect.height);
            var valueRect = new Rect(rect.x + 140, rect.y, rect.width - 140, rect.height);

            GUI.Label(labelRect, "ȸ�� ���");
            healTarget = (EAgentTarget)EditorGUI.EnumPopup(valueRect, healTarget);

            if (healTarget != EAgentTarget.Myself)
            {
                labelRect.y += 20;
                valueRect.y += 20;
                GUI.Label(labelRect, "����");
                radius = EditorGUI.FloatField(valueRect, radius);
            }

            labelRect.y += 20;
            valueRect.y += 20;
            GUI.Label(labelRect, "������ ��� Ÿ��");
            healType = (EPercentageType)EditorGUI.EnumPopup(valueRect, healType);

            labelRect.y += 20;
            valueRect.y += 20;
            GUI.Label(labelRect, "ȸ����");
            damageAmountPer = EditorGUI.FloatField(valueRect, damageAmountPer);

            labelRect.y += 20;
            valueRect.y += 20;
            GUI.Label(labelRect, "FX");
            fx = (FX)EditorGUI.ObjectField(valueRect, fx, typeof(FX), false);
        }

        public override int GetNumRows()
        {
            int rowNum = 4;

            if (healTarget != EAgentTarget.Myself)
            {
                rowNum += 1;
            }

            return rowNum;
        }
#endif
    }
}