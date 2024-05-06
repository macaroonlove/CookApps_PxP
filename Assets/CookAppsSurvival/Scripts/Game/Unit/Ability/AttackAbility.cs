using FrameWork.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    public abstract class AttackAbility : MonoBehaviour
    {
        [SerializeField, ReadOnly] protected bool isAttackAble;
        [SerializeField, ReadOnly] protected float cooldownTime = 0;

        protected int _pureATK;
        protected float _pureAttackTerm;
        protected float _pureAttackDistance;

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
    }
}