using OneDevApp.ImagePicker;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace OneDevApp.ImagePicker_Demo
{
    public class NativeImagePickerDemo : MonoBehaviour
    {
        public Button choicePickerBtn;
        public Button openGalleryBtn;
        public Button openCameraBtn;
        public Image selectedImage;

        private void OnEnable()
        {
            // Subscribe for events from NativeManager
            NativeImagePickerManager.OnImagePicked += OnImagePicked;
        }

        private void OnDisable()
        {
            // Unsubscribe for events from NativeManager
            NativeImagePickerManager.OnImagePicked -= OnImagePicked;
        }
        private void Start()
        {

            choicePickerBtn.onClick.AddListener(() =>
            {
                NativeImagePickerManager.Instance.GetImageFromDevice();
            });

            openGalleryBtn.onClick.AddListener(() =>
            {
                NativeImagePickerManager.Instance.GetImageFromDevice(ImagePickerType.GALLERY);
            });

            openCameraBtn.onClick.AddListener(() =>
            {
                NativeImagePickerManager.Instance.GetImageFromDevice(ImagePickerType.CAMERA);
            });


            NativeImagePickerManager.Instance.PluginDebug(true);
        }


        private void OnImagePicked(ImageData imageData, string errorMessage, ImagePickerErrorCode errorCode)
        {
            if (errorCode == ImagePickerErrorCode.NONE)
            {               
                Debug.Log("OnImagePicked::success");

                try
                {

                    string extension = Path.GetExtension(imageData.cacheFilePath).ToLowerInvariant();
                    TextureFormat format = (extension == ".jpg" || extension == ".jpeg") ? TextureFormat.RGB24 : TextureFormat.RGBA32;

                    int w = imageData.width;
                    int h = imageData.height;

                    Texture2D result = new Texture2D(w, h, format, true, false);

                    if (result.LoadImage(File.ReadAllBytes(imageData.cacheFilePath), true))
                    {
                        Sprite newSprite = Sprite.Create(result as Texture2D, new Rect(0f, 0f, result.width, result.height), Vector2.zero);
                        selectedImage.sprite = newSprite;
                        selectedImage.gameObject.SetActive(true);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogException(e);

                }
                finally
                {

                    try
                    {
                        File.Delete(imageData.cacheFilePath);
                    }
                    catch { }
                }
            }
            else
            {
                Debug.Log("OnImagePicked::failed::" + errorMessage);
            }
        }
    }

}