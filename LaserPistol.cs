using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class LaserPistol : MonoBehaviour
{
    XRGrabInteractable m_InteractableBase;

    public float damage;
    public float range;
    public Transform firePosition;
    public GameObject impactEffect;
    public GameObject bulletPrefab;

    public AudioClip impact;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        m_InteractableBase = GetComponent<XRGrabInteractable>();
        m_InteractableBase.onSelectExit.AddListener(DroppedGun);
        m_InteractableBase.onActivate.AddListener(TriggerPulled);
        m_InteractableBase.onDeactivate.AddListener(TriggerReleased);
    }

    void TriggerReleased(XRBaseInteractor obj)
    {
    }

    void TriggerPulled(XRBaseInteractor obj)
    {
        RaycastFire();
    }

    void DroppedGun(XRBaseInteractor obj)
    {
    }

    void Update()
    {

    }

    /// <summary>
    /// This code determines if the ray gun hits an enemy or a rock and applies the appropriate amount of damage depending on which object is hit.
    /// The laser pistol is for shooting aliens only, no destroying objects.
    /// </summary>
    public void RaycastFire()
    {
        audioSource.PlayOneShot(impact, 0.3F);
        RaycastHit hit;
        if (Physics.Raycast(firePosition.transform.position, firePosition.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);

            // This is commented because I decided that the laser pistol should only be able to hit/damage enemy aliens and not rocks.
            //ObjectRecieveDamage rock = hit.transform.GetComponent<ObjectRecieveDamage>();
            //if (rock != null)
            //{
            //    rock.TakeDamage(.3f);
            //}

            // If we hit an enemy we call the enemies ApplyDamage() function.
            NPCEnemy enemy = hit.transform.GetComponent<NPCEnemy>();
            if (enemy != null)
            {
                enemy.ApplyDamage(damage);
            }

            GameObject bulletObject = Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);

            GameObject impactGO =  Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 1f);
            Destroy(bulletObject, 1f);
        }
    }
}
