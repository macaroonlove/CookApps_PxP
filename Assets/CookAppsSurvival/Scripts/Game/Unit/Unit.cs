using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CookApps.Game
{
    /// <summary>
    /// ��� ������ ����
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Billboarding))]
    [RequireComponent(typeof(UnitAnimationController))]
    [RequireComponent(typeof(MoveAbility))]
    [RequireComponent(typeof(HealthAbility))]
    [RequireComponent(typeof(AbnormalStatusAbility))]
    public abstract class Unit : MonoBehaviour
    {
        protected UnitAnimationController _animationController;
        protected MoveAbility _moveAbility;
        protected HealthAbility _healthAbility;
        protected AbnormalStatusAbility _abnormalStatusAbility;

        public UnitAnimationController animationController => _animationController;
        public MoveAbility moveAbility => _moveAbility;
        public HealthAbility healthAbility => _healthAbility;
        public AbnormalStatusAbility abnormalStatusAbility => _abnormalStatusAbility;


        /// <summary>
        /// ���� ���ݷ�
        /// </summary>
        public abstract int pureATK { get; }
        /// <summary>
        /// ���� �ִ� ü��
        /// </summary>
        public abstract int pureMaxHp { get; }
        
        /// <summary>
        /// ���� ���� �ֱ�
        /// </summary>
        public abstract float pureAttackTerm { get; }
        
        /// <summary>
        /// ���� ���� ����
        /// </summary>
        public abstract float pureAttackRange { get; }
        
        /// <summary>
        /// ���� �̵� �ӵ�
        /// </summary>
        public abstract float pureMoveSpeed { get; }
        

        protected void Initialize()
        {
            if (_animationController == null)
            {
                TryGetComponent(out _animationController);
            }
            
            if (_moveAbility == null)
            {
                TryGetComponent(out _moveAbility);
            }
            
            if (_healthAbility == null)
            {
                TryGetComponent(out _healthAbility);
            }

            if (_abnormalStatusAbility == null)
            {
                TryGetComponent(out _abnormalStatusAbility);
            }

            _animationController.Initialze(this);
            _moveAbility.Initialize(this);
            _healthAbility.Initialize(this);
            _abnormalStatusAbility.Initialize(this);

            _healthAbility.onDeath += OnDeath;
        }

        protected virtual void OnDeath()
        {
            // �״� �ִϸ��̼�
            animationController.Death();

            DeInitialize();
        }

        public virtual void DeInitialize()
        {
            _animationController.DeInitialize();
            _moveAbility.DeInitialize();
            _healthAbility.DeInitialize();
            _abnormalStatusAbility.DeInitialize();

            _healthAbility.onDeath -= OnDeath;
        }
    }
}
