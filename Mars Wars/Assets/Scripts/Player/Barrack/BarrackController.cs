using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BarrackController : MonoBehaviour, InputController.ClickListener, ActionsController.DeselectListener
{   
    public GameObject m_actionsControllerObj;
    public GameObject m_inputControllerObj;
    private ActionsBarrackController m_actionsBarrackController;
    private InputController m_inputController;

    // Use this for initialization
    void Start()
    {
        m_actionsBarrackController = m_actionsControllerObj.GetComponent<ActionsBarrackController>();
        m_inputController = m_inputControllerObj.GetComponent<InputController>();
        m_inputController.AddClickListener(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Cancel()
    {

    }

    public void AcceptClickEvent(Vector3 worldPosition, bool uiClick, Transform hitTransform)
    {
        if (hitTransform == transform)
        {
            m_actionsBarrackController.GetComponent<ActionsBarrackController>().SelectBarrack(gameObject);
        }
    }

    public void OnDeselect()
    {
        
    }
}