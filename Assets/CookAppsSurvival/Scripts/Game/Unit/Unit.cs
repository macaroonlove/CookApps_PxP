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
    public class Unit : MonoBehaviour
    {
        protected UnitAnimationController _animationController;
        protected MoveAbility _moveAbility;

        // healthAbility�� HP�� �������� �Ǵ��ϵ��� ����
        internal bool isDie;

        public UnitAnimationController animationController => _animationController;
        public MoveAbility moveAbility => _moveAbility;

        public virtual void Initialize()
        {
            TryGetComponent(out _animationController);
            TryGetComponent(out _moveAbility);

            _animationController.Initialze(this);
            _moveAbility.Initialize(this);
        }
    }
}
