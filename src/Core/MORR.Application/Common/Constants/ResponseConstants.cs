namespace MORR.Application.Common.Constants
{
    public class ResponseConstants
    {
        public const string USER_DOES_NOT_EXIST_RESPONSE = "User does not exist in the system";
        public const string PASSWORD_INCORRECT_RESPONSE = "Password is incorrect";

        #region Product
        public const string PRODUCT_SAVE_SUCCESS_RESPONSE_MESSAGE =
                           "Product saved has been successfully";
        public const string PRODUCT_UPDATE_SUCCESS_RESPONSE_MESSAGE =
                           "Product updated has been  successfully";
        public const string PRODUCT_NOT_AVAILABLE_RESPONSE_MESSAGE =
                          "Cannot find Product please try again";
        public const string PRODUCT_DELETE_SUCCESS_RESPONSE_MESSAGE =
                          "Product deleted has been  successfully";

        public const string PRODUCT_ITEM_CODE_ALLREADY_EXISTS_RESPONSE_MESSAGE =
                           "Item code is already exists";
        #endregion



        public const string COMMON_EXCEPTION_RESPONSE = "Error has been occurred please try again";

    }
}
