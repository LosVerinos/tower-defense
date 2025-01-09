using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraControllerScript : MonoBehaviour
{
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;
    public float scrollSpeed = 5000f;
    private float yMin = 20f;
    private float yMax = 95f;
    private float zMin;
    private float zMax;
    private float xMin;
    private float xMax;


    void Start(){
        zMin = transform.position.z;
    }


    // Update is called once per frame
    void Update()
    {
        if( Input.GetKey(KeyCode.UpArrow)
            //|| Input.GetKey("z")
            //|| Input.mousePosition.y >= Screen.height - panBorderThickness
        ){

            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }

        if( Input.GetKey(KeyCode.DownArrow)
            //|| Input.GetKey("s")
            //|| Input.mousePosition.y <= panBorderThickness
        ){
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }

        if( Input.GetKey(KeyCode.RightArrow)
            //|| Input.GetKey("d")
            //|| Input.mousePosition.x >= Screen.width - panBorderThickness
        ){
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        if( Input.GetKey(KeyCode.LeftArrow)
            //|| Input.GetKey("q")
            //|| Input.mousePosition.x <= panBorderThickness
        ){
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
    

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;

        
        pos.y -= scroll * scrollSpeed * Time.deltaTime;
        pos.y = Math.Clamp(pos.y, yMin, yMax);

        zMax = CalculateZMax(pos.y);
        pos.z = Mathf.Clamp(pos.z, zMin, zMax);

        xMin = CalculateXMin(pos.y);
        xMax = CalculateXMax(pos.y);

        pos.x = Mathf.Clamp(pos.x, xMin, xMax);

        transform.position = pos;
    }

    float CalculateZMax(float cameraY){
        float mZMax = -92f / 75f; // Pente
        float bZMin = 61.53f; // Ordonnée à l'origine
        return mZMax * cameraY + bZMin;
    }

    float CalculateXMin(float cameraY)
    {
        float mXMin = 44f / 75f; // Pente pour X_min
        float bXMin = -56.765f; // Ordonnée à l'origine pour X_min
        return mXMin * cameraY + bXMin;
    }

    float CalculateXMax(float cameraY)
    {
        float mXMax = -44f / 75f; // Pente pour X_max
        float bXMax = 56.765f; // Ordonnée à l'origine pour X_max
        return mXMax * cameraY + bXMax;
    }
}
