# UnityNativeImagePicker
Unity plugin helps you pick an image fromCamera or Gallery/Photos from your device
<br><br><br>

## INSTALLATION
There are 4 ways to install this plugin:

1. import NativeImagePicker.unitypackage via Assets-Import Package
2. clone/download this repository and move the Plugins folder to your Unity project's Assets folder
3. via Package Manager (add the following line to Packages/manifest.json):

    "com.onedevapp.nativeimagepicker": "https://github.com/onedevapp/UnityNativeImagePicker.git",
4. via Package Manager (Add package from git url):

    https://github.com/onedevapp/UnityNativeImagePicker.git


   
<br><br>

### Requirements
* You project should build against Android 5.0 (API level 21) SDK at least.
* This plugin uses a custom tool for dependency management called the [Play Services Resolver](https://github.com/googlesamples/unity-jar-resolver)


**NOTE:** <br>
According to the Unity3D docs [here](https://docs.unity3d.com/Manual/PluginsForAndroid.html?_ga=2.55742827.1931527617.1606199410-1875972592.1543254704):
AndroidManifest.xml file placed in the `Assets->Plugins->Android` folder (placing a custom manifest completely overrides the default Unity Android manifest). 

Or select an existing manifest from `Project Settings->Publishing Settings->Build->Custom Main Manifest`
<br><br><br>

If your project doesn't have an AndroidManifest, you can copy Unity's default one from `C:\Program Files\Unity\Editor\Data\PlaybackEngines\AndroidPlayer\Apk`. 

Or select from plugins `\Assets\NativeImagePicker\Plugins\Android`
<br><br><br>

To get image from device via Camera or Gallery

```C#	
  	//Call all default values
	//pickerType = ImagePickerType.CHOICE
	//maxWidth = 612
	//maxHeight = 816
	//quality = 80
	NativeImagePickerManager.Instance.GetImageFromDevice();
```
or 
```C#	
  	//Calling required types
	//CHOICE = 0, CAMERA = 1, GALLERY = 2
	NativeImagePickerManager.Instance.GetImageFromDevice(ImagePickerType pickerType = ImagePickerType.CHOICE, int maxWidth = 612, int maxHeight = 816, int quality = 80);
```
-	Callbacks
```C#
	//Register for action	
	NativeImagePickerManager.OnImagePicked += OnImagePicked;
	NativeImagePickerManager.OnImagePickedError += OnImagePickedError;
```


### Debug
-	Toggle library logs
	```C#
	//By default puglin console log will be diabled, but can be enabled
	NativeImagePickerManager.Instance.PluginDebug(bool showLog);
	```
<br><br>

## :open_hands: Contributions
Any contributions are welcome!

1. Fork it
2. Create your feature branch (git checkout -b my-new-feature)
3. Commit your changes (git commit -am 'Add some feature')
4. Push to the branch (git push origin my-new-feature)
5. Create New Pull Request

<br><br>
