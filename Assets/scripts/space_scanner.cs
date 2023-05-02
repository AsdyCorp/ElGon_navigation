using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.IO;
using UnityEditor.VersionControl;
using System.Threading;

public class space_scanner : MonoBehaviour
{

    public int sizeSensorX; 
    public int sizeSensorY;

    public int sizePinX;
    public int sizePinY;

    public int FOV;

    public float scanDistance;
    public float deadZone;


    public Camera _camera;

    public float[,] distanceMap;

    public int[,] distanceMiniMap;

    public GameObject mapVisualiserObject;

    public GameObject mapMiniVisualiserObject;

    RaycastHit hit;
    Ray ray;

    SerialPort stream;


    private void Start()
    {
        _camera.fieldOfView = FOV;
        distanceMap = new float[sizeSensorX, sizeSensorY];
        distanceMiniMap = new int[sizePinX, sizePinY];
        OpenPort();
    
    }

    void OpenPort()
    {
        stream = new SerialPort("COM4", 9600);
        stream.ReadTimeout = 50;
        stream.Open();
    }


    // send pin matrix element vy elemnt to controller
    void Send2DMapToController()
    {
        for (int i = 0; i < sizePinX; i++)
            {
                for (int j = 0; j < sizePinY; j++)
                {
                var dataArray = new byte[] { (byte)distanceMiniMap[i, j] };
                stream.Write(dataArray,0,1);
                }

            }
        var endSymbol = new byte[] { (byte)23 }; // end symbol 
        stream.Write(endSymbol, 0, 1);
        stream.BaseStream.Flush();  
    }


    //
    // Compression of sensor image to pin matrix 
    private void GenerateMiniMap()
    {
        Texture2D texture = new Texture2D(sizePinX, sizePinY, TextureFormat.R16, false);

        // TODO better alghoritm for compression
        for (int i = 0; i < sizePinX; i++)
        {
            for (int j = 0; j < sizePinY; j++)
            {
                float sum = 0; 
                for( int k=0; k< sizeSensorX/sizePinX; k++)
                {
                    for( int  l=0; l< sizeSensorY/sizePinY; l++)
                    {
                        sum += distanceMap[i*(sizeSensorX / sizePinX) + k, j*(sizeSensorY / sizePinY) + l];
                    }
                }
                float avarage = sum / (((int)sizeSensorX / sizePinX) * ((int)sizeSensorY / sizePinY));
                if (avarage < 128)
                {
                    distanceMiniMap[i, j] = 255;
                }
                else 
                {
                    distanceMiniMap[i, j] = 0;
                }
                Color color = new Color(distanceMiniMap[i, j], 0, 0);
                texture.SetPixel(i, j, color);
            }
        }

        texture.Apply();
        
        mapMiniVisualiserObject.GetComponent<Renderer>().material.mainTexture = texture;
        Send2DMapToController();

    }


    // Sensor image from camera (100*100)
    void GetSensorImage()
    {
        Texture2D texture = new Texture2D(sizeSensorX, sizeSensorY, TextureFormat.R16, false);

        for (int i = 0; i < sizeSensorX; i++)
        {
            for (int j = 0; j < sizeSensorY; j++)
            {
                ray = _camera.ScreenPointToRay(new Vector3((int)Mathf.Round(Screen.width / sizeSensorX) * i + 1, (int)Mathf.Round(Screen.height / sizeSensorY) * j + 1, 0));
                //Debug.DrawRay(ray.origin, ray.direction * scanDistance, Color.yellow);
                if (Physics.Raycast(ray, out hit) && hit.distance < scanDistance)
                {
                    distanceMap[i, j] = 0; 
                }
                else
                {
                    distanceMap[i, j] = 255;
                }
                Color color = new Color((float)distanceMap[i, j] / (float)scanDistance, 0, 0);
                texture.SetPixel(i, j, color);
            }

        }

        texture.Apply();

        mapVisualiserObject.GetComponent<Renderer>().material.mainTexture = texture;

        GenerateMiniMap();
    }


    private void FixedUpdate()
    {
        GetSensorImage();
    }

}
