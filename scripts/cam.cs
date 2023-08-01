using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{
    public Transform target1;
    public Transform target2;
    public Transform target3;

    public float interpolationSpeed = 5f;
    public float maxDistanceHeightFactor = 1.5f;
    
    private float _oldPosy;
    private Vector3 _averagePosition;
    private void Update()
    {
        if (target1 == null && target2 == null & target3 == null)
        {
            transform.Translate(Vector3.forward * (6 * Time.deltaTime));
            return;
        }
        if (target1 == null)
        {
            if (target2 == null)
            {
                target2 = target3;
            }
            target1 = target2;
        }

        if (target2 == null)
        {
            if (target3 == null)
            {
                target3 = target1;
            }
            target2 = target3;
        }

        if (target3 == null)
        {
            if (target1 == null)
            {
                target1 = target2;
            }
            target3 = target1;
        }
        _averagePosition = (target1.position + target2.position + target3.position) / 3f;
        // Calculate the distance between the two farthest targets
        float maxDistance = Mathf.Max(
            Vector3.Distance(target1.position, target2.position),
            Vector3.Distance(target2.position, target3.position),
            Vector3.Distance(target3.position, target1.position)
        );
        
        //_oldPosy = _averagePosition.y;
        /*if (_averagePosition.y < _oldPosy + 10)
        {
            _averagePosition.y = _oldPosy + 10;
        }else if (_averagePosition.y > 100)
        {
            _averagePosition.y = 100;
        }*/
        
        _averagePosition.y = _averagePosition.y + maxDistance / maxDistanceHeightFactor;
        _averagePosition.y = _averagePosition.y + 30f;
        // Interpolate the camera's position towards the average position
        transform.position = Vector3.Lerp(transform.position, _averagePosition, interpolationSpeed * Time.deltaTime);
    }
}
