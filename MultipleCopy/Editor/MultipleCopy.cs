using UnityEngine;
using UnityEditor;
using UnityEditor.Presets;
using System.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace EditorSaveComponent
{
    [InitializeOnLoad]
    public class MultipleCopy
    {
        //static string path = "/Data/new_DataComponents.asset";

        public static string path
        {
            get
            {
                var g = AssetDatabase.FindAssets("t:Script MultipleCopy");
                string newPath = AssetDatabase.GUIDToAssetPath(g[0]);
                return newPath.Replace("MultipleCopy.cs", "Data/DataComponents.asset"); 
            }
        }

       


        [MenuItem("Tools/MultipleCopy/Apply Data Components")]
        static void LoadDataComponents()
        {
            DataComponents m_DataComponents = (DataComponents)AssetDatabase.LoadAssetAtPath(path, typeof(DataComponents));

            //string bbb = EditorUtility.GetAssetPath(DataComponents)
            for (int i = 0; i < m_DataComponents.allDataComponents.Count; i++)
            {
                Undo.RecordObject(m_DataComponents.allDataComponents[i].m_Component, "Changed Info Component");

                m_DataComponents.allDataComponents[i].infoComponent.ApplyTo(m_DataComponents.allDataComponents[i].m_Component);
            }
            //CreateFileData(true);
        }
        [MenuItem("Tools/MultipleCopy/Clear Data Components")]
        static void ClearDataComponents()
        {
            DataComponents m_DataComponents = (DataComponents)AssetDatabase.LoadAssetAtPath(path, typeof(DataComponents));
            m_DataComponents.allDataComponents.Clear();
            m_DataComponents.allDataComponents = new List<DataComponent>();
            //CreateFileData(true);
        }
        [MenuItem("Tools/MultipleCopy/Clear Data Components", true), MenuItem("Tools/MultipleCopy/Apply Data Components",true)]
        static bool CheckData()
        {

            if (!File.Exists(path)) return false;
            DataComponents m_DataComponents = (DataComponents)AssetDatabase.LoadAssetAtPath(path, typeof(DataComponents));
            return m_DataComponents.allDataComponents.Count > 0;
        }

        [MenuItem("CONTEXT/Object/Copy MultipleCopy")]
        static void SaveDataComponent(MenuCommand command)
        {
            //EditorApplication.isPaused = true;

            CreateFileData();
            AddComponent((Component)command.context);
        }

        [MenuItem("CONTEXT/Object/Paste MultipleCopy")]
        static void PasteDataComponent(MenuCommand command)
        {

            DataComponents m_DataComponents = (DataComponents)AssetDatabase.LoadAssetAtPath(path, typeof(DataComponents));
            int indexComponent = m_DataComponents.CheckDataComponent((Component)command.context).index;
            Undo.RecordObject(m_DataComponents.allDataComponents[indexComponent].m_Component, "Changed Info Component");
            m_DataComponents.allDataComponents[indexComponent].infoComponent.ApplyTo(m_DataComponents.allDataComponents[indexComponent].m_Component);
        }
        [MenuItem("CONTEXT/Object/Paste MultipleCopy", true)]
        static bool CheckPasteDataComponent(MenuCommand command)
        {

            if (!File.Exists(path)) return false;
            DataComponents m_DataComponents = (DataComponents)AssetDatabase.LoadAssetAtPath(path, typeof(DataComponents));
            return m_DataComponents.CheckDataComponent((Component)command.context).isExists;
        }

        [MenuItem("CONTEXT/Object/Clear MultipleCopy")]
        static void ReomoveDataComponent(MenuCommand command)
        {

            DataComponents m_DataComponents = (DataComponents)AssetDatabase.LoadAssetAtPath(path, typeof(DataComponents));
            m_DataComponents.ReomveInfoComponent((Component)command.context);
        }
        [MenuItem("CONTEXT/Object/Clear MultipleCopy", true)]
        static bool CheckReomoveDataComponent(MenuCommand command)
        {

            if (!File.Exists(path)) return false;
            DataComponents m_DataComponents = (DataComponents)AssetDatabase.LoadAssetAtPath(path, typeof(DataComponents));
            return m_DataComponents.CheckDataComponent((Component)command.context).isExists;
        }



     

        static void CreateFileData(bool delet = false)
        {
            if (!delet)
                if (File.Exists(path)) return;

            DataComponents m_DataComponents = ScriptableObject.CreateInstance<DataComponents>();
            AssetDatabase.CreateAsset(m_DataComponents, path);
            m_DataComponents.allDataComponents = new List<DataComponent>();
            AssetDatabase.SaveAssets();

        }
        static void AddComponent(Component newComponent)
        {
            DataComponents m_DataComponents = (DataComponents)AssetDatabase.LoadAssetAtPath(path, typeof(DataComponents));
            Preset newPerest = new Preset(newComponent);
            m_DataComponents.AddNewData(newComponent, newPerest);
            EditorUtility.SetDirty(m_DataComponents);
            //Selection.activeObject = last;
            //AssetDatabase.Refresh();
        }



    }
}

