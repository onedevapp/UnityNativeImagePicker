using System;
using UnityEngine;

namespace OneDevApp.ImagePicker
{

    /// <summary>
    /// ImageData class model for sharing image details
    /// </summary>
    [Serializable]
    public class ImageData
    {
        public bool status;
        public string message;
        public int errorCode;
        public int width;
        public int height;
        public int mimeType;
        public int orientation;
        public string uri;
        public string path;
        public string cacheFilePath;
    }

    public class NativeImagePickerManager : MonoBehaviour
    {

        #region Events

#pragma warning disable 0067
        /// <summary>
        /// Event triggered with image picked
        /// </summary>
        public static event Action<ImageData, string, ImagePickerErrorCode> OnImagePicked;

#pragma warning restore 0067
        #endregion

        public static NativeImagePickerManager Instance { get; private set; }

#pragma warning disable 0414
        /// <summary>
        /// UnityMainActivity current activity name or main activity name
        /// Modify only if this UnityPlayer.java class is extends or used any other default class
        /// </summary>
        [Tooltip("Android Launcher Activity")]
        [SerializeField]
        private string m_unityMainActivity = "com.unity3d.player.UnityPlayer";

        bool writeLog = false;

#pragma warning restore 0414

#if UNITY_ANDROID && !UNITY_EDITOR
        private AndroidJavaObject mContext = null;
        private AndroidJavaObject mUpdateManager = null;

        class OnImageSelectedListener : AndroidJavaProxy
        {
            public OnImageSelectedListener() : base("com.onedevapp.nativeimagepicker.OnImageSelectedListener") { }

            public void onImageSelected(bool status, string message, int errorCode)
            {
                if(OnImagePicked != null)
                {	
                
                    UnityMainThreadDispatcher.Instance().Enqueue(() => {

                        ImageData imageData = null;
                        if (status)
                        {
                            Debug.Log("OnImagePicked::message::" + message);
                            imageData = JsonUtility.FromJson<ImageData>(message);
                            message = string.Empty;
                        }

                        OnImagePicked.Invoke(imageData, message, (ImagePickerErrorCode)errorCode);
                    });
                }
            }
        }
#endif

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                DestroyImmediate(Instance.gameObject);
                Instance = this;
            }

#if UNITY_ANDROID && !UNITY_EDITOR
            if (Application.platform == RuntimePlatform.Android)
            {
                mContext = new AndroidJavaClass(m_unityMainActivity).GetStatic<AndroidJavaObject>("currentActivity");
            }
#elif UNITY_EDITOR
            if (writeLog)
                Debug.Log("Platform not supported");
#endif
        }


        #region ImagePicker

        /// <summary>
        /// Get image from device via Camera or Gallery
        /// </summary>
        /// <param name="pickerType">ImagePickerType of Choice or Camera or Gallery</param>
        /// <param name="maxWidth">image max width to compress</param>
        /// <param name="maxHeight">image max height to compress</param>
        /// <param name="quality">image quality from 1 to 100</param>
        public void GetImageFromDevice(ImagePickerType pickerType = ImagePickerType.CHOICE, int maxWidth = 612, int maxHeight = 816, int quality = 80)
        {

#if UNITY_ANDROID && !UNITY_EDITOR
            // Initialize ImagePicker Manager

            using (AndroidJavaClass jc = new AndroidJavaClass("com.onedevapp.nativeimagepicker.ImagePickerManager"))
            {
                var mImagePickerManager = jc.CallStatic<AndroidJavaObject>("Builder", mContext);
                mImagePickerManager
                    .Call<AndroidJavaObject>("setPickerType", (int)pickerType)
                    .Call<AndroidJavaObject>("setMaxWidth", maxWidth)
                    .Call<AndroidJavaObject>("handler", new OnImageSelectedListener())
                    .Call<AndroidJavaObject>("setMaxHeight", maxHeight)
                    .Call<AndroidJavaObject>("setQuality", quality)
                    .Call("openImagePicker");
            }
#elif UNITY_EDITOR
            if (writeLog)
                Debug.Log("Platform not supported");
#endif
        }

        #endregion


        #region Debug
        /// <summary>
        /// By default puglin console log will be diabled, but can be enabled
        /// </summary>
        /// <param name="showLog">If set true then log will be displayed else disabled</param>
        public void PluginDebug(bool showLog = true)
        {
#if UNITY_ANDROID && !UNITY_EDITOR

            AndroidJNIHelper.debug = showLog;
            var constantClass = new AndroidJavaClass("com.onedevapp.nativeimagepicker.Constants");
            constantClass.SetStatic("enableLog", showLog);

#elif UNITY_EDITOR
            writeLog = showLog;
#endif
        }
        #endregion
    }

}