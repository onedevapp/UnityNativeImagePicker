namespace OneDevApp.ImagePicker
{
    /// <summary>
    /// Image picker options
    /// </summary>
    public enum ImagePickerType
    {
        // choice if he user wants to choose
        CHOICE = 0,
        // Opens camera
        CAMERA = 1,
        // Opens gallery
        GALLERY = 2
    }

    /// <summary>
    /// Status of a error.
    /// </summary>
    public enum ImagePickerErrorCode
    {
        NONE = 0,
        ERROR_PERMISSION_FAILED = 1,
        ERROR_FILE_NOT_READABLE = 2,
        ERROR_INTERNAL_ERROR = 4,
        ERROR_FILE_CANT_CREATE = 5,
    }
}
