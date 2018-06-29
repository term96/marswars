using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HospitalController : MonoBehaviour
{
    public float m_cooldown;
    public float m_increaseHealthValue;
    float m_currentCooldown;

    private UnitController m_unitController;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        m_currentCooldown -= Time.deltaTime;
        if (m_currentCooldown <= 0)
        {
            Increase();
            m_currentCooldown = m_cooldown;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            m_unitController = (UnitController)other.GetComponent<UnitController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            m_unitController = null;
        }
    }

    private void Increase()
    {
        if (m_unitController != null)
        {
            m_unitController.IncreaseHealth(m_increaseHealthValue);
        }
    }

}