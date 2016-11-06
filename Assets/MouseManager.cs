using UnityEngine;
using System.Collections;

public class MouseManager : MonoBehaviour {
    public LayerMask snapPointLayerMask;
	// Use this for initialization
	void Start () {
	
	}

    public GameObject thePrefabToSpawn;
	
	// Update is called once per frame
	void Update () {
	
        // Was the mouse pressed down this frame?

        if(Input.GetMouseButtonDown(0))
        {
            // Yes, the left mouse button was pressed this frame
            // Is the mouse over a cube

           
            Camera theCamera = Camera.main;

            Ray ray = theCamera.ScreenPointToRay( Input.mousePosition );

            RaycastHit hitInfo;
            
            if( Physics.Raycast( ray, out hitInfo ) )
            {
                int maskForLayer = 1 << hitInfo.collider.gameObject.layer;
                // did we click on a snap point
                if ((maskForLayer & snapPointLayerMask)!=0)
                {
                    Vector3 spawnSpot = hitInfo.collider.transform.position;

                    Quaternion spawnRotation = hitInfo.collider.transform.rotation;

                    GameObject obj = Instantiate(thePrefabToSpawn, spawnSpot, spawnRotation) as GameObject;
                    obj.transform.SetParent(hitInfo.collider.transform);
                    // disable the renderer of snap point
                    if (hitInfo.collider.GetComponent<Renderer>() != null)
                    {
                        hitInfo.collider.GetComponent<Renderer>().enabled = false;
                    }

                    if(obj.transform.parent.GetComponent<Collider>()!=null)
                    {
                        obj.transform.parent.GetComponent<Collider>().enabled = true;
                    }
                }

            }
        }
	}
    void RemovePart(GameObject obj)
    {
        //reenable meshrenderer
        obj.transform.parent.GetComponent<Renderer>().enabled = true;
        obj.transform.parent.GetComponent<Collider>().enabled = true;
        Destroy(obj);
    }

}
