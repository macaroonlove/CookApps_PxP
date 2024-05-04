using FrameWork.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    public abstract class AttackAbility : MonoBehaviour
    {
        protected float _cooldownTime = 0;
        protected float _pureAttackTerm;
        protected float _pureAttackDistance;

        /// <summary>
        /// ���� ���� ����
        /// </summary>
        protected abstract float finalAttackTerm { get; }

        protected virtual void Update()
        {
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