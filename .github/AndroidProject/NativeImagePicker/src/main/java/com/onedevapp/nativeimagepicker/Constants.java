package com.onedevapp.nativeimagepicker;

import android.content.Context;
import android.content.pm.PackageManager;
import android.os.Build;
import android.util.Log;

public class Constants {
    public static final int REQUEST_TAKE_PHOTO = 9877;   //Code for request to open camera.
    public static final int REQUEST_GALLERY_PHOTO = 9878;    //Code for request to open gallery.

    public static final int EC_IMAGE_PICKER_PERMISSION_FAILED = 1;
    public static final int EC_IMAGE_PICKER_FILE_NOT_READABLE = 2;
    public static final int EC_IMAGE_PICKER_INTERNAL_ERROR = 4;
    public static final int EC_IMAGE_PICKER_FILE_CANT_CREATE = 5;


    /**
     * To write library messages to logcat
     */
    public static boolean enableLog = false;

    /**
     * WriteLog to log library messages to logcat
     * Can toggle on/off with enableLog boolean at any time
     *
     * @param message Log Message
     */
    public static void WriteLog(String message) {
        if (enableLog) Log.d("NativePlugin", message);
    }


    /**
     * Check device build version
     *
     * @return true if build version is over Marshmallow else false
     */
    public static boolean isOverMarshmallow() {
        return Build.VERSION.SDK_INT >= Build.VERSION_CODES.M;
    }

    /**
     * This method checks whether the given permission is already granted or not.
     *
     * @param context    This is context of the current activity
     * @param permission This is the permission we need to check
     * @return boolean     Returns True if already permission granted for this permission else false.
     */
    public static boolean CheckPermission(Context context, String permission) {
        if (!isOverMarshmallow()) {
            return false;
        }
        //Determine whether you have been granted a particular permission.
        return context.checkSelfPermission(permission) == PackageManager.PERMISSION_GRANTED;
    }
}
