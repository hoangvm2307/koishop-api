using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishopBusinessObjects.Constants
{
    public static class ExceptionConstants
    {
        public const string USER_NOT_EXIST = "User is not existed: ";

        #region Invalid number
        public const string INVALID_PRICE = "Invalid price input";
        #endregion

        #region Koifish exception
        public const string INVALID_KOIFISH_GENDER = "Invalid fish gender";
        public const string INVALID_KOIFISH_TYPE = "Invalid fish type";
        public const string INVALID_KOIFISH_STATUS = "Invalid fish status";
        public const string KOIFISH_NOT_EXIST = "Koifish is not existed: ";
        #endregion

        #region Rating exception
        public const string RATING_NOT_EXIST = "Rating is not existed: ";
        public const string INVALID_RATING_VALUE = "Invalid rating value! Value must be greater than 0 and less than 5";
        #endregion

        #region
        public const string INVALID_ORDER_STATUS = "Invalid order status";
        public const string INVALID_ORDER_TOTAL_AMOUNT = "Invalid total amount";
        #endregion
    }
}
