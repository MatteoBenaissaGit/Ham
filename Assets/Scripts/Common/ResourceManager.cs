using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MatteoBenaissaLibrary.SingletonClassBase;
using UnityEditor;
using UnityEngine;

namespace Common
{
    public class ResourceManager : Singleton<ResourceManager>
    {
        [field:Header("Resources")] [field:SerializeField] public List<GameObject> Resources { get; private set; } = new List<GameObject>();
        
        protected override void InternalAwake()
        {
            
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            string enumName = "ResourceEnum";
            string[] enumEntries = Resources.Select(x => x.name).ToArray();
            string filePathAndName = "Assets/Scripts/Common/" + enumName + ".cs";

            using (StreamWriter streamWriter = new StreamWriter(filePathAndName))
            {
                streamWriter.WriteLine("public enum " + enumName);
                streamWriter.WriteLine("{");
                for (int i = 0; i < enumEntries.Length; i++)
                {
                    streamWriter.WriteLine("	" + enumEntries[i] + ",");
                }

                streamWriter.WriteLine("}");
            }

            AssetDatabase.Refresh();
        }
#endif
        
        public GameObject InstantiateResource(ResourceEnum resource)
        {
            GameObject go = Resources[(int)resource];
            if (go == null)
            {
                throw new Exception("no resource for this");
            }
            return Instantiate(go);
        }
        
        public GameObject InstantiateResource(ResourceEnum resource, Vector3 position)
        {
            GameObject go = Resources[(int)resource];
            if (go == null)
            {
                throw new Exception("no resource for this");
            }
            return Instantiate(go, position, Quaternion.identity);
        }
    }
}