using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using UnityEditor.Presets;
using UnityEditor;
//[CreateAssetMenu(fileName = "DataOfComponent_", menuName = "Create Data Game/ Data Of Component")]

[InitializeOnLoad]
public class DataComponents : ScriptableObject
{
   
    public List<DataComponent> allDataComponents;

    public void AddNewData(Component new_Component , Preset new_infoComponent)
    {
        (bool isExists, int index) isExistsComponent = CheckDataComponent(new_Component);
        if (isExistsComponent.isExists)
        {
            allDataComponents[isExistsComponent.index] = new DataComponent { m_Component = new_Component, infoComponent = new_infoComponent };

        }
        else
        {
            allDataComponents.Add(new DataComponent {  m_Component = new_Component, infoComponent = new_infoComponent });

        }
    }
    public (bool isExists, int index) CheckDataComponent(Component new_Component)
    {
        for (int i = 0; i < allDataComponents.Count; i++)
        {
            if (new_Component.Equals(allDataComponents[i].m_Component)) return (true, i);
        }
        return (false, -1);
    }

    public void ReomveInfoComponent(Component new_Component)
    {
        (bool isExists, int index) isExistsComponent = CheckDataComponent(new_Component);
        if (isExistsComponent.isExists)
        {
            allDataComponents.RemoveAt(isExistsComponent.index);

        }
    }
}
[System.Serializable]
public struct DataComponent
{
   public Component m_Component;
   public Preset infoComponent;
}