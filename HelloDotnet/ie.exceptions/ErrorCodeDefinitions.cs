using System;

namespace ie.errorcodes 
{
    public class Base {
        public const string ILLEGAL_ARGUMENT_ERROR = "99998";
        public const string FAIL_TO_WRITE_TO_FILE = "99997";
        public const string FAIL_TO_READ_FROM_FILE = "99996";
        public const string RUN_PREDICATION_ERROR = "99995";
        public const string JSON_WRAPPING_FAILURE = "99989";
        public const string JSON_PARSING_FAILURE = "99988";
        public const string NO_SPECIFIED_KEY_IN_JSON = "99987";
        public const string AES_ENCRYPTION_ERROR = "99979";
        public const string AES_DECRYPTION_ERROR = "99978";
        public const string IMAGE_CONVERSION_ERROR = "99959";
        public const string DATE_ITEM_GENERATION_FAILURE = "99958";
        public const string DISPLAY_ERROR_CODE_AND_MESSAGE = "99957";
        public const string XML_PARSING_FAILURE = "99956";
        public const string FACEBOOK_GRAPH_RESPONSE_ERROR = "99955";
        public const string FACEBOOK_LOGIN_ERROR = "99954";
        public const string RX_MAYBE_ON_COMPLETE = "99953";
        public const string OUT_OF_MEMORY = "99952";
        public const string UNSUPPORTED_ENCODING = "99951";
        public const string INVALID_REQUEST_CODE = "99950";
        public const string NETWORK_IS_DISCONNECTED = "99949";
        public const string THREAD_INTERRUPTED = "99948";
        public const string NULL_POINTER_ERROR = "99947";
        public const string DEFAULT_UNIT_TEST_ERROR = "99946";
        public const string INTERNAL_GENERATION_ERROR = "99945";
        public const string INTERNAL_CONVERSION_ERROR = "99944";
        public const string INTERNAL_FILTERING_ERROR = "99943";
        public const string INTERNAL_PROCESS_ERROR = "99942";
    }
    
    public class Ping {
        public const string TIMEOUT = "99799";
        public const string INTERRUPTED = "99798";
        public const string IO_EXCEPTION = "99797";
        public const string ALL_PACKETS_GOT_LOST = "99796";
        public const string PARTIAL_PACKETS_GOT_LOST = "99795";
        public const string UNKNOWN_HOST = "99794";
        public const string EXIT_VALUE_ONE = "99793";
        public const string EXIT_VALUE_OTHERS = "99792";
        public const string INDEX_ERROR = "99791";
        public const string REACH_RETRY_LIMIT = "99790";
    }

    public class Sql {
        public const string OPERATION_FAILURE = "99699";
        public const string OBJECT_DOES_NOT_EXIST = "99698";
        public const string NO_KEY_JSON_FOR_OBJECT = "99697";
        public const string QUERY_FAILURE = "99696";
        public const string DELETE_FAILURE = "99695";
        public const string INSERT_FAILURE = "99694";
        public const string UPDATE_FAILURE = "99693";
        public const string CURSOR_CONVERSION_ERROR = "99692";
        public const string HAS_EXISTED_IN_SQL = "99691";
    }

    public class Http {
        public const string FAIL_TO_EXECUTE_API_REQUEST = "99599";
        /** catch the {@link java.io.IOException} */
        public const string REQUEST_ERROR = "99598";
        public const string RESPONSE_ERROR = "99597";
        public const string RESPONSE_PARSING_ERROR = "99596";
        public const string REQUEST_WRAPPING_FAILURE = "99595";
        public const string REQUEST_PARAMETER_EXAMINATION_FAILURE = "99594";
        public const string SERVER_INVALID_ACCESS_TOKEN = "99593";
        public const string WRONG_STATUS_CODE = "99592";
        public const string MALFORMED_URL = "99591";
    }

    public class Socket {
        public const string CREATION_FAILURE = "99499";
        public const string CONNECTION_FAILURE = "99498";
        public const string CONNECTION_TIMEOUT = "99497";
        public const string SOCKET_HAS_BEEN_CLOSED = "99496";
        public const string CHANNEL_REGISTRATION_FAILURE = "99495";
        public const string FAIL_TO_SEND_OUT_PACKET = "99494";
        public const string FAIL_TO_READ_FROM_OUT_PACKET = "99493";
        public const string INVALID_INCOMING_PACKET_FORMAT = "99492";
        public const string INVALID_CALIBRATION_CHANNEL_VALUE = "99491";
        public const string SELECTOR_IS_NULL = "99490";
        public const string NO_SELECTED_RESULT = "99489";
        public const string NO_WORKABLE_RESULT = "99488";
        public const string READ_TIMEOUT = "99487";
        public const string GENERAL_OPERATION_FAILURE = "99401";
    }

    public class Ftp {
        public const string CONNECTION_FAILURE = "99399";
        public const string  CONNECTION_TIMEOUT = "99398";
        public const string  LOGIN_FAILURE = "99397";
        public const string  LOGOUT_FAILURE = "99396";
        public const string  CHANGE_DIRECTORY_FAILURE = "99395";
        public const string  FAIL_TO_GET_FILE_LIST = "99394";
        public const string  DOWNLOAD_FAILURE = "99393";
        public const string  UPLOAD_FAILURE = "99392";
        public const string  GENERAL_OPERATION_FAILURE = "99379";
    }

    public class WiFi {
        public const string TIMEOUT = "99299";
        public const string REACH_RETRY_LIMIT = "99298";
        /** general case for the device cannot connect to the specific Wi-Fi router  */
        public const string FAIL_TO_CONNECT_TO_SSID = "99297";
        /** used when [android.net.ConnectivityManager.NetworkCallback.onUnavailable] gets called  */
        public const string NETWORK_UNAVAILABLE = "99296";
        /** used when either [android.net.ConnectivityManager.NetworkCallback.onAvailable] or
        * [android.net.ConnectivityManager.NetworkCallback.onCapabilitiesChanged] gets called
        * but the SSID of connected Wi-Fi is not the specific one!
        */
        public const string CONNECTED_SSID_IS_UNEXPECTED = "99295";
    }
}
