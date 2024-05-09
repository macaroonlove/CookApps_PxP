using FrameWork.Editor;
using System.Collections;
using System.Collections.Generic;
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
        [EnumCondition("damageTarget", (int)EEnemyTarget.OneEnemyInRange, (int)EEnemyTarget.NumEnemyInRange, (int)EEnemyTarget.AllEnemyInRange)]
        [SerializeField] private float radius;

        /// <summary>
        /// ������ ���� ��
        /// </summary>
        [EnumCondition("damageTarget", (int)EEnemyTarget.NumEnemyInRange)]
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
        [Space(20), SerializeField] private GameObject _prefab;

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
    }
}
