using FrameWork.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    public abstract class AttackAbility : MonoBehaviour
    {
        [SerializeField] protected bool isProjectileAttack;
        [SerializeField, Condition("isProjectileAttack", true)] protected GameObject projectilePrefab;
        [SerializeField, Condition("isProjectileAttack", true)] protected Transform projectileSpawnPoint;

        [SerializeField, ReadOnly] protected bool isAttackAble;
        [SerializeField, ReadOnly] protected float cooldownTime = 0;

        protected Unit _attackTarget;
        protected PoolSystem _poolSystem;
        protected int _pureATK;
        protected float _pureAttackTerm;
        protected float _pureAttackDistance;

        private AttackEventHandler _attackEventHandler;
        private bool _isEventAttack;

        public abstract Unit unit { get; }

        #region ���� �߻� �޼���
        /// <summary>
        /// ���� ���� ����
        /// </summary>
        internal abstract float finalAttackTerm { get; }

        /// <summary>
        /// ���� ���ݰŸ�
        /// </summary>
        internal abstract float finalAttackDistance { get; }

        /// <summary>
        /// ���� ���ݷ�
        /// </summary>
        internal abstract int finalATK { get; }
        #endregion

        internal void Initialize()
        {
            _poolSystem = BattleManager.Instance.GetSubSystem<PoolSystem>();

            _attackEventHandler = GetComponentInChildren<AttackEventHandler>();
            if (_attackEventHandler != null)
            {
                _attackEventHandler.onAttack += OnAttackEvent;
                _isEventAttack = true;
            }
        }

        internal virtual void DeInitialize()
        {
            if (_attackEventHandler != null)
            {
                _attackEventHandler.onAttack -= OnAttackEvent;
                _isEventAttack = false;
            }
        }

        protected virtual void Update()
        {
            if (!isAttackAble) return;

            //���ݻ��� �ð� ���
            if (cooldownTime > 0)
            {
                cooldownTime -= Time.deltaTime;
                return;
            }

            //���� ���ɻ���
            bool isExcute = Action();
            if (isExcute)
            {
                cooldownTime = finalAttackTerm;
            }
        }

        protected abstract bool Action();

        protected virtual void AttackAnimation(Unit attackTarget)
        {
            // ���� ���
            unit.animationController.Attack();

            if (!_isEventAttack)
            {
                Attack(attackTarget);
            }            
        }

        private void OnAttackEvent()
        {
            Attack(_attackTarget);
        }

        private void Attack(Unit attackTarget)
        {
            // ����ü ������ ���
            if (isProjectileAttack)
            {
                // ����ü ����
                var projectile = _poolSystem.Spawn(projectilePrefab).GetComponent<Projectile>();
                projectile.transform.SetPositionAndRotation(projectileSpawnPoint.position, Quaternion.identity);
                projectile.Initialize(this, attackTarget);
            }
            // ��� ������ ���
            else
            {
                AttackImpact(attackTarget);
            }
        }

        internal virtual void AttackImpact(Unit attackTarget)
        {
            // Ÿ�ٿ��� ������ �ֱ�
            attackTarget.healthAbility.Damaged(finalATK, unit.id);
        }

        protected virtual bool IsInRange(Unit attackTarget)
        {
            float distance = Vector3.Distance(unit.transform.position, attackTarget.transform.position);

            return distance <= finalAttackDistance;
        }

        internal void DeSpawnProjectile(GameObject obj)
        {
            _poolSystem.DeSpawn(obj);
        }
    }
}