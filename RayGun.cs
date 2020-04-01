using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class RayGun : MonoBehaviour
{
    XRGrabInteractable m_InteractableBase;
    
    [SerializeField] ParticleSystem m_RayParticleSystem = null;

    const float k_HeldThreshold = 0.1f;

    public float damage = 10f;
    public float range = 50f;
    public Transform firePosition;
    public GameObject impactEffect;

    float m_TriggerHeldTime;
    bool m_TriggerDown;

    void Start()
    {
        m_InteractableBase = GetComponent<XRGrabInteractable>();
        m_InteractableBase.onSelectExit.AddListener(DroppedGun);
        m_InteractableBase.onActivate.AddListener(TriggerPulled);
        m_InteractableBase.onDeactivate.AddListener(TriggerReleased);
    }

    void TriggerReleased(XRBaseInteractor obj)
    {
        m_TriggerDown = false;
        m_TriggerHeldTime = 0;
        m_RayParticleSystem.Stop();
    }

    void TriggerPulled(XRBaseInteractor obj)
    {
        m_TriggerDown = true;
    }

    void DroppedGun(XRBaseInteractor obj)
    {
        m_TriggerDown = false;
        m_TriggerHeldTime = 0;
        m_RayParticleSystem.Stop();
    }

    void Update()
    {
        if (m_TriggerDown)
        {
            m_TriggerHeldTime += Time.deltaTime;
            RaycastFire();
            if (m_TriggerHeldTime >= k_HeldThreshold)
            {
                if (!m_RayParticleSystem.isPlaying)
                {
                    m_RayParticleSystem.Play();
                }
            }
        }
    }

    public void ShootEvent()
    {
        m_RayParticleSystem.Emit(10);
    }

    /// <summary>
    /// This code determines if the ray gun hits an enemy or a rock and applies the appropriate amount of damage depending on which object is hit.
    /// </summary>
    public void RaycastFire()
    {
        RaycastHit hit;
        if (Physics.Raycast(firePosition.transform.position, firePosition.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            ObjectRecieveDamage rock = hit.transform.GetComponent<ObjectRecieveDamage>();
            if (rock != null)
            {
                rock.TakeDamage(.3f);
            }

            
            NPCEnemy enemy = hit.transform.GetComponent<NPCEnemy>();
            if (enemy != null)
            {
                enemy.ApplyDamage(1f);
            }

            GameObject impactGO =  Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 1f);
        }
    }
}
