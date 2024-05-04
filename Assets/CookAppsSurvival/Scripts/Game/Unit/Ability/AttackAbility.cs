using FrameWork.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    public abstract class AttackAbility : MonoBehaviour
    {
        [SerializeField, ReadOnly] protected bool _isAttackAble;
        [SerializeField, ReadOnly] protected float _cooldownTime = 0;

        protected int _pureATK;
        protected float _pureAttackTerm;
        protected float _pureAttackDistance;
        
        /// <summary>
        /// ���� ���� ����
        /// </summary>
        protected abstract float finalAttackTerm { get; }

        /// <summary>
        /// ���� ���ݰŸ�
        /// </summary>
        protected abstract float finalAttackDistance { get; }

        protected virtual void Update()
        {
            if (!_isAttackAble) return;

            //���ݻ��� �ð� ���
            if (_cooldownTime > 0)
            {
                _cooldownTime -= Time.deltaTime;
                return;
            }

            //���� ���ɻ���
            bool isExcute = Action();
            if (isExcute)
            {
                _cooldownTime = finalAttackTerm;
            }
        }

        protected abstract bool Action();
    }
}