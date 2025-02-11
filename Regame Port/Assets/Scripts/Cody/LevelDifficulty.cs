﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LevelDifficulty : MonoBehaviour
{
    [Header("Target")]
    public GameObject target;
    public Vector3 calibratedTargetPos;
    [Header("Throwable Object")]
    public GameObject objectToThrow;
    [Header("Obstacle")]
    public GameObject obstaclePrefab;
    [Header("Obstacle Spawn Position")]
    public Transform obstacleLocation;
    public Transform startPos;
    public Transform endPos;
    [Header("Adjustable Values")]
    public float AdjustableTargetPercent = 2.0f;
    public float AdjustableObjectPercent = 1f;
    public float obstacleSpeed = 1.0f;
    public GlobalControl globalControl;
    public bool hasRecievedBaseline = false;
    private GameObject _obstacle;
    public TextMeshProUGUI instructionsPanel;
    public Gameplay gameplay;
    public AudioManager AudioManager;
    public bool hasPlayedGreen = false;
    public bool hasPlayedYellow = false;
    public bool hasPlayedRed = false;
    public bool hasPlayedOrange = false;
    public bool hasPlayedLastRound = false;
    


    private void Update()
    {
        if (globalControl.hasCalibrated && !hasRecievedBaseline)
        {
            calibratedTargetPos = target.transform.position;
            hasRecievedBaseline = true;
        }
    }

    public void MoveTarget()
    {
        target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z * AdjustableTargetPercent);
    }

    public void AdjustTargetSize(float reductionPercent)
    {
        target.transform.localScale = Vector3.one * reductionPercent;
    }

    public void AdjustObjectSize(float reductionPercent)
    {
        objectToThrow.transform.localScale = Vector3.one * reductionPercent;
    }

    public void SpawnObstacle()
    {
        _obstacle = Instantiate(obstaclePrefab, obstacleLocation);
    }

    public void DestroyObstacle()
    {
        if(_obstacle != null)
        {
            Destroy(_obstacle);
        }
    }

    public void MoveObstacle()
    {
        obstaclePrefab.transform.position = Vector3.Lerp(startPos.position, endPos.position, (Mathf.Sin(obstacleSpeed * Time.time) + 1.0f) / 2.0f);
    }

    public void LevelOne()
    {
        ResetTargetPosition();
        ResetTargetSize();
        globalControl.currentLevel = "1";
        instructionsPanel.text = !gameplay.hasCompletedFinalLevel ? "Press the green button to start this level." : "TEST";
    }

    public void LevelTwo()
    {
        ResetTargetPosition();
        ResetTargetSize();

        MoveTarget();
        globalControl.currentLevel = "2";

        instructionsPanel.text = !gameplay.hasCompletedFinalLevel ? "Press the red button to start this level." : "TEST";
    }

    public void LevelThree()
    {
        ResetTargetSize();
        AdjustTargetSize(AdjustableObjectPercent);
        ResetTargetPosition();
        globalControl.currentLevel = "3";
        
        instructionsPanel.text = !gameplay.hasCompletedFinalLevel ? "Press the yellow button to start this level." : "TEST";
        
    }

    public void LevelFour()
    {
        //Reset positions and size for reference
        ResetTargetPosition();
        ResetTargetSize();
        MoveTarget();
        AdjustTargetSize(AdjustableObjectPercent);
        globalControl.currentLevel = "4";
        
        instructionsPanel.text = !gameplay.hasCompletedFinalLevel ? "Press the orange button to start this level." : "TEST";
    }

    public void LevelFive()
    {
        ResetTargetPosition();
        ResetTargetSize();

        MoveTarget();
        AdjustTargetSize(AdjustableObjectPercent);
        SpawnObstacle();
        globalControl.currentLevel = "5";
        
        instructionsPanel.text = !gameplay.hasCompletedFinalLevel ? "This is the last part! Please touch the color that the researcher suggested." : "TEST";
    }

    //Used to reset target reference
    public void ResetTargetPosition()
    {
        target.transform.position = calibratedTargetPos;
    }

    public void ResetTargetSize()
    {
        target.transform.localScale = Vector3.one * 1f;
    }

    public void FullReset()
    {
        ResetTargetPosition();
        ResetTargetSize();
        DestroyObstacle();
    }
}
