﻿using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

using EasyBuildSystem.Features.Runtime.Buildings.Manager;
using EasyBuildSystem.Features.Runtime.Buildings.Part;
using EasyBuildSystem.Features.Runtime.Buildings.Placer;

public class HorizontalBuildingMenu : MonoBehaviour
{
    #region Fields 

    [Serializable]
    public class UIBuildingSlot
    {
        [SerializeField] Image m_Icon;
        public Image Icon { get { return m_Icon; } set { m_Icon = value; } }

        BuildingPart m_BuildingPart;
        public BuildingPart BuildingPart { get { return m_BuildingPart; } set { m_BuildingPart = value; } }
    }

    [SerializeField] Transform m_UIBuildPanel;

    [SerializeField] Text m_UISelectionText;

    [SerializeField] UIBuildingSlot m_PreviousBuildingSlot;
    [SerializeField] UIBuildingSlot m_CurrentBuildingSlot;
    [SerializeField] UIBuildingSlot m_NextBuildingSlot;

#if ENABLE_INPUT_SYSTEM
    [SerializeField] Key m_PlacingModeActionKey = Key.B;
    [SerializeField] Key m_PreviousSelectionActionKey = Key.Q;
    [SerializeField] Key m_NextSelectionActionKey = Key.E;
#else
    [SerializeField] KeyCode m_PlacingModeActionKey = KeyCode.B;
    [SerializeField] KeyCode m_PreviousSelectionActionKey = KeyCode.Q;
    [SerializeField] KeyCode m_NextSelectionActionKey = KeyCode.E;
#endif

    int m_Index;

    #endregion

    #region Unity Methods

    void Update()
    {
        if (BuildingManager.Instance == null)
        {
            return;
        }

#if ENABLE_INPUT_SYSTEM

        if (Keyboard.current[m_PlacingModeActionKey].wasPressedThisFrame)
        {
            BuildingPlacer.Instance.ChangeBuildMode(BuildingPlacer.BuildMode.PLACE);
            Refresh();
        }

        if (BuildingPlacer.Instance.GetBuildMode == BuildingPlacer.BuildMode.PLACE)
        {
            if (Keyboard.current[m_PreviousSelectionActionKey].wasPressedThisFrame)
            {
                ChangeIndex(false);
            }

            if (Keyboard.current[m_NextSelectionActionKey].wasPressedThisFrame)
            {
                ChangeIndex(true);
            }
        }
#else
        if (Input.GetKeyDown(m_PlacingModeActionKey))
        {
            BuildingPlacer.Instance.ChangeBuildMode(BuildingPlacer.BuildMode.PLACE);
            Refresh();
        }

        if (BuildingPlacer.Instance.GetBuildMode == BuildingPlacer.BuildMode.PLACE)
        {
            if (Input.GetKeyDown(m_PreviousSelectionActionKey))
            {
                ChangeIndex(false);
            }

            if (Input.GetKeyDown(m_NextSelectionActionKey))
            {
                ChangeIndex(true);
            }
        }
#endif

        m_UIBuildPanel.gameObject.SetActive(BuildingPlacer.Instance.GetBuildMode == BuildingPlacer.BuildMode.PLACE);
    }

    #endregion

    #region Internal Methods

    void ChangeIndex(bool increase)
    {
        if (increase)
        {
            m_Index++;

            if (m_Index >= BuildingManager.Instance.BuildingPartReferences.Count)
            {
                m_Index = 0;
            }
        }
        else
        {
            m_Index--;

            if (m_Index < 0)
            {
                m_Index = BuildingManager.Instance.BuildingPartReferences.Count - 1;
            }
        }

        Refresh();
    }

    void Refresh()
    {
        List<BuildingPart> buildingParts = BuildingManager.Instance.BuildingPartReferences;

        BuildingPart buildingPart = buildingParts[GetPreviousIndex()];
        Texture2D icon = buildingPart.GetGeneralSettings.Thumbnail;
        m_PreviousBuildingSlot.Icon.sprite = Sprite.Create(icon, new Rect(0f, 0f, icon.width, icon.height), new Vector2(0.5f, 0.5f), 100f);

        buildingPart = buildingParts[m_Index];
        icon = buildingPart.GetGeneralSettings.Thumbnail;
        m_CurrentBuildingSlot.Icon.sprite = Sprite.Create(icon, new Rect(0f, 0f, icon.width, icon.height), new Vector2(0.5f, 0.5f), 100f);

        m_UISelectionText.text = buildingPart.GetGeneralSettings.Name;

        buildingPart = buildingParts[GetNextIndex()];
        icon = buildingPart.GetGeneralSettings.Thumbnail;
        m_NextBuildingSlot.Icon.sprite = Sprite.Create(icon, new Rect(0f, 0f, icon.width, icon.height), new Vector2(0.5f, 0.5f), 100f);

        BuildingPlacer.Instance.ChangeBuildMode(BuildingPlacer.BuildMode.PLACE);
        BuildingPlacer.Instance.SelectBuildingPart(BuildingManager.Instance.BuildingPartReferences[m_Index]);
    }

    int GetPreviousIndex()
    {
        int index = m_Index;

        if (index - 1 < 0)
        {
            return BuildingManager.Instance.BuildingPartReferences.Count - 1;
        }
        else
        {
            return index - 1;
        }
    }

    int GetNextIndex()
    {
        int index = m_Index;

        if (index + 1 >= BuildingManager.Instance.BuildingPartReferences.Count)
        {
            return 0;
        }

        return index + 1;
    }

    #endregion
}