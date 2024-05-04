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
    public class Unit : MonoBehaviour
    {
        protected UnitAnimationController _animationController;

        public UnitAnimationController animationController => _animationController;

        protected virtual void Awake()
        {
            TryGetComponent(out _animationController);
            
        }

        void Start()
        {
            
        }
    }
}
