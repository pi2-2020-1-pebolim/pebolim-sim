﻿using PUDM.DataObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGrabber : MonoBehaviour
{
    [SerializeField]
    int targetFrameRate;

    float timeBetweenFrames;
    float timeAccumulator;

    [SerializeField]
    Vector2Int targetResolution;

    private bool takeScreenshot = false;

    UnityEngine.Camera captureCamera;

    void Start() {
        
        captureCamera = GetComponent<UnityEngine.Camera>();
        captureCamera.enabled = true;
        captureCamera.targetTexture = new RenderTexture(targetResolution.x, targetResolution.y, 24, RenderTextureFormat.ARGB32);

        timeBetweenFrames = 1.0f / targetFrameRate;
        timeAccumulator = 0;
    }

    void Update() {

        timeAccumulator += Time.deltaTime;

        if (timeAccumulator >= timeBetweenFrames)
        {
            //PrepareCaptureFrame();
            takeScreenshot = true;
            timeAccumulator -= timeBetweenFrames;
        }

    }

    private void LateUpdate() {
        
        if (takeScreenshot) {
          
            var defaultRenderTexture = RenderTexture.active;
            RenderTexture.active = captureCamera.targetTexture;
            
            Texture2D screenShot = new Texture2D(targetResolution.x, targetResolution.y, TextureFormat.ARGB32, false);
            screenShot.ReadPixels(new Rect(0, 0, targetResolution.x, targetResolution.y), 0, 0);
            screenShot.Apply();

            RenderTexture.active = defaultRenderTexture;
            
            byte[] bytes = screenShot.EncodeToJPG();
            
            Destroy(screenShot);
            
            GameManager.Instance.SendUpdate(bytes);
            takeScreenshot = false;
        }
    }

    public CameraSettings GetCameraSettings() {
        return new CameraSettings(
            this.targetFrameRate,
            new Tuple<int, int>(this.targetResolution.x, this.targetResolution.y)
        );
    }
}
