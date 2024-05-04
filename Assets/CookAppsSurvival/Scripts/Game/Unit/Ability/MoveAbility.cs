using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CookApps.Game
{
    /// <summary>
    /// 파티원 및 적 유닛이 파티의 리더를 쫓아가도록 하는 클래스
    /// </summary>
    public class MoveAbility : MonoBehaviour
    {
        [SerializeField] private Transform _targetPos;
        [SerializeField] private bool _isReverse;

        private Unit _unit;
        private NavMeshAgent _agent;
        private PartySystem _partySystem;
        private EnemySystem _enemySystem;

        private bool _isMove;
        private Unit _target;

        public bool isMove => _isMove;

        public Unit target => _target;
        public Transform targetPos => _targetPos;

        public void Initialize(Unit unit)
        {
            _unit = unit;

            if (TryGetComponent(out _agent))
            {
                _agent.enabled = true;
            }
            else
            {
                Debug.LogError("네브메시 에이전트를 찾지 못했습니다.\n" +
                    "해당 오브젝트를 비활성화 합니다.");
                gameObject.SetActive(false);
            }

            _partySystem = BattleManager.Instance.GetSubSystem<PartySystem>();
            _enemySystem = BattleManager.Instance.GetSubSystem<EnemySystem>();

            _partySystem.onUnitRevival += NewTarget;
            _partySystem.onUnitDie += NewTarget;

            NewTarget(_partySystem.mainUnit);
        }

        void NewTarget(PartyUnit mainUnit)
        {
            if (_unit != mainUnit)
            {
                _targetPos = mainUnit.transform;
                _target = null;
            }
            else
            {
                _targetPos = null;
            }
        }

        public void NewAttackTarget(Unit target)
        {
            _targetPos = target.transform;

            PartyUnit partyUnit = (_unit as PartyUnit);
            if (partyUnit != null)
            {
                switch (partyUnit.template.job)
                {
                    case EJob.Archer:
                    case EJob.Priest:
                        _agent.stoppingDistance = 6;
                        break;
                }
            }
        }

        void Update()
        {
            if (_unit.isDie) return;

            // 타겟이 없을 경우
            if (_targetPos == null)
            {
                // 자신을 기준으로 가장 가까운 적을 찾기
                _target = _enemySystem.GetNearestEnemy(_unit.transform.position);
                _targetPos = _target.transform;
            }

            // 이동
            if (_agent != null && _targetPos != null)
            {
                _agent.SetDestination(_targetPos.position);

                if (_agent.isActiveAndEnabled && !_agent.isStopped && _agent.velocity.magnitude > 0.3f)
                {
                    _isMove = true;
                }
                else
                {
                    _isMove = false;
                }
            }

            // 회전
            FlipUnit();
        }

        internal bool IsUnitLeft()
        {
            Vector3 directionToUnit = _unit.transform.position - _targetPos.position;
            Vector3 unitRight = _unit.transform.forward;
            float angle = Vector3.SignedAngle(directionToUnit, unitRight, Vector3.up);

            return angle > 0f;
        }

        void FlipUnit()
        {
            bool isLeft = IsUnitLeft();

            float scaleX = isLeft ^ _isReverse ? 1f : -1f;
            transform.DOScaleX(scaleX, 0.1f);
        }
    }
}
