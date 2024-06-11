using FrameWork.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        [SerializeField] private float radius;

        /// <summary>
        /// �����̻� �ɸ��� �� ���� ��
        /// </summary>
        [SerializeField] private int numberOfEnemies;

        /// <summary>
        /// �����̻� ����
        /// </summary>
        [SerializeField] private AbnormalStatusTemplate abnormalStatus;

        /// <summary>
        /// �����̻� ���ӽð�
        /// </summary>
        [SerializeField] private float duration;
        
        public override List<Unit> GetTarget(PartyUnit unit)
        {
            List<Unit> enemies = new List<Unit>();
            enemies.AddRange(unit.agentAttackAbility.FindAttackTargetEnemies(abnormalTarget, radius, numberOfEnemies));

            return enemies;
        }

        public override void Excute(PartyUnit unit, Unit enemy)
        {
            if (unit == null || enemy == null) return;
            if (!enemy.healthAbility.IsAlive) return;

            enemy.abnormalStatusAbility.ApplyAbnormalStatus(abnormalStatus, duration);
        }

        public override string GetLabel()
        {
            return "��� �����̻� ��ų";
        }

#if UNITY_EDITOR
        public override void Draw(Rect rect)
        {
            var labelRect = new Rect(rect.x, rect.y, 140, rect.height);
            var valueRect = new Rect(rect.x + 140, rect.y, rect.width - 140, rect.height);

            GUI.Label(labelRect, "���� ���");
            abnormalTarget = (EEnemyTarget)EditorGUI.EnumPopup(valueRect, abnormalTarget);

            if (abnormalTarget != EEnemyTarget.AllEnemy)
            {
                labelRect.y += 20;
                valueRect.y += 20;
                GUI.Label(labelRect, "����");
                radius = EditorGUI.FloatField(valueRect, radius);
            }

            if (abnormalTarget == EEnemyTarget.NumEnemyInRange)
            {
                labelRect.y += 20;
                valueRect.y += 20;
                GUI.Label(labelRect, "������ ���� ��");
                numberOfEnemies = EditorGUI.IntField(valueRect, numberOfEnemies);
            }            

            labelRect.y += 20;
            valueRect.y += 20;
            GUI.Label(labelRect, "������ ��� Ÿ��");
            abnormalStatus = (AbnormalStatusTemplate)EditorGUI.ObjectField(valueRect, abnormalStatus, typeof(AbnormalStatusTemplate), false);

            labelRect.y += 20;
            valueRect.y += 20;
            GUI.Label(labelRect, "�����̻� ���ӽð�");
            duration = EditorGUI.FloatField(valueRect, duration);
        }

        public override int GetNumRows()
        {
            int rowNum = 3;

            if (abnormalTarget != EEnemyTarget.AllEnemy)
            {
                rowNum += 1;
            }

            if (abnormalTarget == EEnemyTarget.NumEnemyInRange)
            {
                rowNum += 1;
            }

            return rowNum;
        }
#endif
    }
}
