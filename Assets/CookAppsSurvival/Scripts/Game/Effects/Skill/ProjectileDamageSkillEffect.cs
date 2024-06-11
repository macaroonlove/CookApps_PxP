using FrameWork.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CookApps.Game
{
    [CreateAssetMenu(menuName = "CookAppsSurvival/Effects/Skill/ProjectileDamage", fileName = "Skill_ProjectileDamage", order = 0)]
    public class ProjectileDamageSkillEffect : SkillEffect
    {
        /// <summary>
        /// ���� ���
        /// </summary>
        [SerializeField] private EEnemyTarget damageTarget;

        /// <summary>
        /// ���� ����
        /// </summary>
        [SerializeField] private float radius;

        /// <summary>
        /// ������ ���� ��
        /// </summary>
        [SerializeField] private int numberOfEnemies;

        /// <summary>
        /// ������ ��� Ÿ��
        /// </summary>
        [SerializeField] private EPercentageType damageType;

        /// <summary>
        /// ���ط�
        /// </summary>
        [SerializeField] private float damageAmountPer;

        /// <summary>
        /// ������
        /// </summary>
        [SerializeField] private GameObject _prefab;

        /// <summary>
        /// ����ü ���� ��ġ���� ������ ������
        /// </summary>
        [SerializeField] private bool isUseProjectileSpwanPoint;

        /// <summary>
        /// ���� ��ġ ������
        /// </summary>
        [SerializeField] private Vector3 _offset;

        /// <summary>
        /// fx
        /// </summary>
        [SerializeField] FX muzzleFx;
        [SerializeField] FX hitFx;

        public override List<Unit> GetTarget(PartyUnit unit)
        {
            List<Unit> enemies = new List<Unit>();
            enemies.AddRange(unit.agentAttackAbility.FindAttackTargetEnemies(damageTarget, radius, numberOfEnemies));

            return enemies;
        }

        public override void Excute(PartyUnit unit, Unit enemy)
        {
            if (_prefab == null) return;
            if (enemy == null || unit == null) return;
            if (!enemy.healthAbility.IsAlive) return;

            Vector3 spawnPoint = unit.transform.position;
            if (isUseProjectileSpwanPoint)
            {
                spawnPoint = unit.attackAbility.projectileSpawnPointVector;
            }

            var poolSystem = BattleManager.Instance.GetSubSystem<PoolSystem>();

            var projectile = poolSystem.Spawn(_prefab).GetComponent<Projectile>();
            projectile.transform.SetPositionAndRotation(spawnPoint + _offset, Quaternion.identity);
            projectile.Initialize(this, unit, enemy);

            
            if (muzzleFx != null)
            {
                muzzleFx.Play(enemy, unit);
            }
        }

        public void SkillImpact(Unit unit, Unit enemy)
        {
            var damage = GetAmount(unit);

            enemy.healthAbility.Damaged(damage, unit.id);

            if (hitFx != null)
            {
                hitFx.Play(enemy, unit);
            }
        }

        public int GetAmount(Unit partyUnit)
        {
            int amount;
            float typeValue = 0f;
            if (damageType == EPercentageType.ATK) typeValue = partyUnit.pureATK;
            else if (damageType == EPercentageType.MaxHP) typeValue = partyUnit.healthAbility.maxHp;

            amount = (int)(typeValue * damageAmountPer);

            return amount;
        }

        public override string GetLabel()
        {
            return "����ü ������ ��ų";
        }

#if UNITY_EDITOR
        public override void Draw(Rect rect)
        {
            var labelRect = new Rect(rect.x, rect.y, 140, rect.height);
            var valueRect = new Rect(rect.x + 140, rect.y, rect.width - 140, rect.height);

            GUI.Label(labelRect, "���� ���");
            damageTarget = (EEnemyTarget)EditorGUI.EnumPopup(valueRect, damageTarget);

            if (damageTarget != EEnemyTarget.AllEnemy)
            {
                labelRect.y += 20;
                valueRect.y += 20;
                GUI.Label(labelRect, "����");
                radius = EditorGUI.FloatField(valueRect, radius);
            }

            if (damageTarget == EEnemyTarget.NumEnemyInRange)
            {
                labelRect.y += 20;
                valueRect.y += 20;
                GUI.Label(labelRect, "������ ���� ��");
                numberOfEnemies = EditorGUI.IntField(valueRect, numberOfEnemies);
            }

            labelRect.y += 20;
            valueRect.y += 20;
            GUI.Label(labelRect, "������ ��� Ÿ��");
            damageType = (EPercentageType)EditorGUI.EnumPopup(valueRect, damageType);

            labelRect.y += 20;
            valueRect.y += 20;
            GUI.Label(labelRect, "���ط�");
            damageAmountPer = EditorGUI.FloatField(valueRect, damageAmountPer);

            labelRect.y += 20;
            valueRect.y += 20;
            GUI.Label(labelRect, "����ü ������");
            _prefab = (GameObject)EditorGUI.ObjectField(valueRect, _prefab, typeof(GameObject), false);
            
            labelRect.y += 20;
            valueRect.y += 20;
            valueRect.x += 20;
            GUI.Label(labelRect, "����ü ���� ��ġ���� ������ ������");
            isUseProjectileSpwanPoint = EditorGUI.Toggle(valueRect, isUseProjectileSpwanPoint);
            valueRect.x -= 20;

            if (!isUseProjectileSpwanPoint)
            {
                labelRect.y += 20;
                valueRect.y += 20;
                labelRect.width += 140;
                _offset = EditorGUI.Vector3Field(labelRect, "������ġ ������", _offset);
                labelRect.width -= 140;
            }

            labelRect.y += 20;
            valueRect.y += 20;
            GUI.Label(labelRect, "Ÿ�� FX");
            muzzleFx = (FX)EditorGUI.ObjectField(valueRect, muzzleFx, typeof(FX), false);

            labelRect.y += 20;
            valueRect.y += 20;
            GUI.Label(labelRect, "�ǰ� FX");
            hitFx = (FX)EditorGUI.ObjectField(valueRect, hitFx, typeof(FX), false);
        }

        public override int GetNumRows()
        {
            int rowNum = 7;

            if (damageTarget != EEnemyTarget.AllEnemy)
            {
                rowNum += 1;
            }

            if (damageTarget == EEnemyTarget.NumEnemyInRange)
            {
                rowNum += 1;
            }

            if (!isUseProjectileSpwanPoint)
            {
                rowNum += 1;
            }

            return rowNum;
        }
#endif
    }
}
