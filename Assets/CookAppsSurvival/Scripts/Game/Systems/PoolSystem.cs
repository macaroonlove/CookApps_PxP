using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    /// <summary>
    /// �����ؾ��ϴ� ����Ʈ�� �����ϴ� Ŭ����
    /// </summary>
    public class PoolSystem : MonoBehaviour, ISubSystem
    {
        private Dictionary<string, Stack<GameObject>> objectPool = new Dictionary<string, Stack<GameObject>>();

        public void Initialize()
        {
            
        }

        public void Deinitialize()
        {
            // ������Ʈ Ǯ ����ֱ�
            foreach (var stack in objectPool.Values)
            {
                foreach (var obj in stack)
                {
                    Destroy(obj);
                }
                stack.Clear();
            }
            objectPool.Clear();
        }

        internal GameObject Spawn(GameObject obj)
        {
            string key = obj.name;

            if (objectPool.ContainsKey(key) && objectPool[key].Count > 0)
            {
                GameObject poolObj = objectPool[key].Pop();
                poolObj.SetActive(true);
                return poolObj;
            }
            else
            {
                GameObject newObj = Instantiate(obj, transform);
                newObj.name = key;

                if (!objectPool.ContainsKey(key))
                {
                    objectPool[key] = new Stack<GameObject>();
                }

                return newObj;
            }
        }

        internal void DeSpawn(GameObject obj)
        {
            string key = obj.name;

            if (!objectPool.ContainsKey(key))
            {
                objectPool[key] = new Stack<GameObject>();
            }

            objectPool[key].Push(obj);
            obj.SetActive(false);
        }
    }
}
