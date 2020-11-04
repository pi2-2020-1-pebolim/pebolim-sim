using PUDM.DataObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraGrabber : MonoBehaviour
{
    [SerializeField]
    int targetFrameRate;

    float timeBetweenFrames;
    float timeAccumulator;

    [SerializeField]
    Vector2Int targetResolution;

    Queue<int> screenshotQueue;

    UnityEngine.Camera captureCamera;

    void Start() {

        screenshotQueue = new Queue<int>();
        
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
            screenshotQueue.Enqueue(0);
            screenshotQueue.Enqueue(1);
            timeAccumulator -= timeBetweenFrames;
        }

    }

    void TakeScreenshot(int player) {

        var defaultRenderTexture = RenderTexture.active;
        RenderTexture.active = captureCamera.targetTexture;

        Texture2D screenShot = new Texture2D(targetResolution.x, targetResolution.y, TextureFormat.ARGB32, false);
        screenShot.ReadPixels(new Rect(0, 0, targetResolution.x, targetResolution.y), 0, 0);
        screenShot.Apply();
        
        RenderTexture.active = defaultRenderTexture;
    
        
        if (player == 1) {
    
            var reversed = FlipTextureVertically(screenShot);
            Destroy(screenShot);
            screenShot = reversed;
            
        }

        byte[] bytes = screenShot.EncodeToJPG();
        var isntance = GameManager.GetInstance(player);
        if (isntance != null) { 
            GameManager.GetInstance(player).SendUpdate(bytes);
        }
        Destroy(screenShot);
    }

    private void LateUpdate() {
        
        if (screenshotQueue.Count > 0) {

            var player = screenshotQueue.Dequeue();
            TakeScreenshot(player);
        }
    }

    public Texture2D FlipTextureVertically(Texture2D original) {
        
        var originalPixels = original.GetPixels();

        Color[] newPixels = new Color[originalPixels.Length];

        int width = original.width;
        int rows = original.height;

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < rows; y++) {
                newPixels[x + y * width] = originalPixels[(width - x - 1) + (rows - y - 1) * width];
            }
        }

        var newTexture = new Texture2D(original.width, original.height);
        newTexture.SetPixels(newPixels);
        newTexture.Apply();

        return newTexture;
    }

    public CameraSettings GetCameraSettings() {
        return new CameraSettings(
            this.targetFrameRate,
            new Tuple<int, int>(this.targetResolution.x, this.targetResolution.y)
        );
    }
}
