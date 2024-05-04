using FrameWork.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CookApps.Game
{
    /// <summary>
    /// �Ʊ� ���� ��Ƽ�� �����ϴ� �ý���
    /// </summary>
    public class PartySystem : MonoBehaviour, ISubSystem
    {
        private List<Transform> _pos = new List<Transform>();

        internal Transform pos 
        {
            get
            {
                foreach(var p in _pos)
                {
                    if (p.gameObject.activeSelf)
                    {
                        return p;
                    }
                }

                // ���� ���� �̺�Ʈ �����ֱ�?(Action)
                return null;
            }
        }

        public void Initialize()
        {
            _pos = transform.GetChild(0).GetComponents<Transform>().ToList();
        }

        public void Deinitialize()
        {
            
        }
    }
}
