using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ArCursor : MonoBehaviour
{

    [SerializeField] private GameObject cursorChildObject;
    [SerializeField] private GameObject objectToPlace;
    [SerializeField] private ARRaycastManager raycastManager;

    public bool useCursor = true;

    private void Start()
    {
        cursorChildObject.SetActive(useCursor);
    }

    private void Update()
    {
        if (useCursor)
        {
            MoveCursor();
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            GameObject.Instantiate(objectToPlace, transform.position, transform.rotation);

        }
        else
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            raycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
            if(hits.Count > 0)
            {
                GameObject.Instantiate(objectToPlace, hits[0].pose.position, hits[0].pose.rotation);
            }
        }
    }

    private void MoveCursor()
    {
        Vector2 screenPos = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenPos, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        if(hits.Count > 0 )
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
        }
    }
}
