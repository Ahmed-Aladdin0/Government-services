namespace Government.Errors
{
    public class ServiceError
    {
        public static readonly Error ServiceNotFound = new Error(Code: "Not Found ", Description: " Service Not Found , Please Enter a Valid ServiceId !! ");
        public static readonly Error FileNotFound = new Error(Code: "Not Found ", Description: " File Not Found , Please Enter a Valid FileId !! ");
        public static readonly Error DuplicatingNameOrDescription = new Error(Code: "ServiceDuplication", Description: " Service Name or Description is already exists , Please Enter another Name or Description for the service !! ");

        
    }
}
