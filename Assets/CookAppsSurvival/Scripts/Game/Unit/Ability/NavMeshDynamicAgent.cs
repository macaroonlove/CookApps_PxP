using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CookApps.Game
{
    /// <summary>
    /// ��Ƽ�� �� �� ������ ��Ƽ�� ����(��Ŀ)�� �Ѿư����� �ϴ� Ŭ����
    /// </summary>
    public class NavMeshDynamicAgent : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        private Unit _unit;
        private NavMeshAgent _agent;

        public void Initialize(Unit unit)
        {
            _unit = unit;

            if (TryGetComponent(out _agent))
            {
                _agent.enabled = true;
            }
            else
            {
                Debug.LogError("�׺�޽� ������Ʈ�� ã�� ���߽��ϴ�.\n" +
                    "�ش� ������Ʈ�� ��Ȱ��ȭ �մϴ�.");
                gameObject.SetActive(false);
            }
            

            if (_target == null)
            {
                // AgentSystem�� ���� ���� ������
            }
        }

        void Update()
        {
            if (_agent != null && _target != null)
            {
                _agent.SetDestination(_target.position);

                if (_agent.isActiveAndEnabled && !_agent.isStopped && _agent.velocity.magnitude > 0.2f)
                {
                    _unit.animationController.Move(true);
                }
                else
                {
                    _unit.animationController.Move(false);
                }
            }
        }
    }
}
