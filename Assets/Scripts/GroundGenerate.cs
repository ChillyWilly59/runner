using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GroundGenerate : MonoBehaviour 
{
    private List<GameObject> ReadyGround = new List<GameObject>();
    
    [Header("Все участки дороги")]
    public GameObject[] Ground;
    public bool[] groundNumber;
    
    [Header("Текущая длина дороги")]
    public int currentGroundLength = 0;
    
    [Header("Максимальная длина дороги")]
    public int maximumGroundLength = 6;
    
    [Header("Дистанция между дорогами")]
    public float distanceBetweenGround;

    [Header("Скорость передвижения дороги")]
    public float speedGround = 5;
    
    [Header("Позиция Z где удаляется дорога")]
    public float maximumPositionZ = -15;

    [Header("Зона ожидания")]
    public Vector3 waitingZona = new Vector3(0, 0, 40);
    
    private int currentGroundNumber = -1;
    private int lastGroundNumber = -1;
    
    [Header("Статус генерации")]
    public string groundGenerationStatus = "Generation";

    private void FixedUpdate()
    {
        if (groundGenerationStatus == "Generation")
        {
            if (currentGroundLength != maximumGroundLength)
            {
                currentGroundNumber = Random.Range(0, Ground.Length);

                if (currentGroundNumber != lastGroundNumber)
                {
                    if (currentGroundNumber < Ground.Length / 2)
                    {
                        if (groundNumber[currentGroundNumber] != true)
                        {
                            if (lastGroundNumber !=(Ground.Length / 2) + currentGroundNumber )
                            {
                                GroundCreation();
                            }
                            else if (lastGroundNumber == (Ground.Length / 2) + currentGroundNumber &&
                                     currentGroundLength == Ground.Length - 1)
                            {
                                GroundCreation();
                            }
                        } 
                    }
                    else if(currentGroundNumber>=Ground.Length/2)
                    {
                        if (groundNumber[currentGroundNumber] != true)
                        {
                            if (lastGroundNumber != currentGroundNumber - (Ground.Length / 2))
                            {
                                GroundCreation();
                            }
                            else if (lastGroundNumber == currentGroundNumber - (Ground.Length / 2) &&
                                     currentGroundLength == Ground.Length - 1)
                            {
                                GroundCreation();
                            }
                        }
                    }
                }
            }

            MovingGround();

            if (ReadyGround.Count != 0)
            {
                RemovingGround();
            }
        }
    }

    private void MovingGround()
    {
        foreach (GameObject readyGround in ReadyGround)
        {
            readyGround.transform.localPosition -= new Vector3(0f, 0f, speedGround * Time.fixedDeltaTime);
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void RemovingGround()
    {
        if (ReadyGround[0].transform.localPosition.z < maximumPositionZ)
        {
            int i;
            i = ReadyGround[0].GetComponent<Ground>().number;
            groundNumber[i] = false;
            ReadyGround[0].transform.localPosition = waitingZona;
            ReadyGround.RemoveAt(0);
            currentGroundLength--;
        } 
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void GroundCreation()
    {
        if (ReadyGround.Count > 0)
        {
            Ground[currentGroundNumber].transform.localPosition
                = ReadyGround[ReadyGround.Count - 1].transform.position + new Vector3(0f, 0f, distanceBetweenGround);
        }
        else if (ReadyGround.Count == 0)
        {
            Ground[currentGroundNumber].transform.localPosition = new Vector3(0f, 0f, 0f);
        }

        Ground[currentGroundNumber].GetComponent<Ground>().number = currentGroundNumber;

        groundNumber[currentGroundNumber] = true;

        lastGroundNumber = currentGroundNumber;
        
        ReadyGround.Add(Ground[currentGroundNumber]);

        currentGroundLength++;
    }
}
