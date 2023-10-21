using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class movement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public Rigidbody rb;
    public ParticleSystem collisionSystem;

    public Slider slider;
    
    public cameraShake shaker;
    public float shakeStrength = 3f;

    public AudioSource hit;

    public playerManager pManager;

    public GameObject deadCube;
    void FixedUpdate()
    {
        rb.AddForce(Vector3.right * (Input.GetAxis("Horizontal") * walkSpeed));
        rb.AddForce(Vector3.forward * (Input.GetAxis("Vertical") * walkSpeed));
    }

    IEnumerator waitTillFreeze()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
        GameObject cube = Instantiate(deadCube);
        cube.transform.position = this.gameObject.transform.position;
        cube.transform.rotation = this.transform.rotation;
        
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (rb.velocity.magnitude > 1)
        {
            shaker.ShakeOnce(1f, shakeStrength, null, Camera.current);
            hit.pitch = Time.timeScale + Random.Range(-0.1f, 0.4f);
            hit.Play();
        }
        else
        {
            hit.volume = 0.1f;
            hit.Play();
            hit.volume = 0.3f;
            shaker.ShakeOnce(1f, shakeStrength / 2, null, Camera.current);
        }
        foreach (ContactPoint contact in other.contacts)
        {
            ParticleSystem sys = Instantiate(collisionSystem);
            sys.transform.position = contact.point;
            sys.Play();
        }
    }

    public cam cam;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LevelEnd"))
        {
            pManager.pEnd++;
        }else if (other.CompareTag("death"))
        {
            StartCoroutine(waitTillFreeze());
        }else if (other.CompareTag("jump"))
        {
            pManager.pJump++;
        }
        else if (other.CompareTag("changeCamValue"))
        {
            cam.maxDistanceHeightFactor = 0.1f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LevelEnd"))
        {
            pManager.pEnd--;
        }else if (other.CompareTag("jump"))
        {
            pManager.pJump--;
        }
    }

    
}
