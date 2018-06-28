using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.AI;

public class ActionsBarrackController : MonoBehaviour, InputController.ClickListener
{
    public Button m_createButton;
    public Button m_cancelButton;
    public GameObject m_inputControllerObj;
    public GameObject prefab;

    public GameObject m_actionsControllerObj;

    private InputController m_inputController;
    private Vector3 m_spawnPosition;

    private void Start()
    {
        m_createButton.gameObject.SetActive(false);
        m_cancelButton.gameObject.SetActive(false);
        m_inputController = m_inputControllerObj.GetComponent<InputController>();
        m_inputController.AddClickListener(this);

        m_spawnPosition = GameObject.FindGameObjectWithTag("PlayerBarrack").transform.position;
        m_spawnPosition = new Vector3(m_spawnPosition.x - 5, m_spawnPosition.y, m_spawnPosition.z);
    }

    public void SelectBarrack(GameObject unit)
    {
        m_createButton.gameObject.SetActive(true);
        m_cancelButton.gameObject.SetActive(true);
    }

    private void Update()
    {

    }

    public void AcceptClickEvent(Vector3 worldPosition, bool uiClick, Transform hitTransform)
    {
        if (!uiClick)
        {

        }
    }

    public void Cancel()
    {
        m_createButton.gameObject.SetActive(false);
        m_cancelButton.gameObject.SetActive(false);
    }

    public void Create()
    {
        Vector3 rayCoord = new Vector3(m_spawnPosition.x, m_spawnPosition.y, 0);
        Ray ray = Camera.main.ScreenPointToRay(rayCoord);

        if (!Physics.Raycast(ray, 100))
        {
            GameObject obj = prefab as GameObject;
            UnitController unit = obj.GetComponent<UnitController>();
            unit.m_actionsControllerObj = m_actionsControllerObj;
            unit.m_inputControllerObj = m_inputControllerObj;

            obj = Instantiate(obj, m_spawnPosition, Quaternion.identity) as GameObject;
        }
    }

    public interface DeselectListener
    {
        void OnDeselect();
    }
}
