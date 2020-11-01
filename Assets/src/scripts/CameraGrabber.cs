using PUDM.DataObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            
            GameManager.GetInstance(0).SendUpdate(bytes);
            if (GameManager.GetInstance(1) != null) {
                var reversed = FlipTextureVertically(screenShot);
                GameManager.GetInstance(1).SendUpdate(reversed.EncodeToPNG());
                Destroy(reversed);
            }

            Destroy(screenShot);
            takeScreenshot = false;
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

        var newTexture = new Texture2D(original.width, original.height, original.format, false);
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
