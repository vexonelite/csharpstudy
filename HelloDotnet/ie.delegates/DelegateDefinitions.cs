using System;

namespace ie.delegates 
{
    public interface CellTypeDelegate {
        int theCellType { get; }    // read-only        
    }

    public interface DescriptionDelegate {    
        string theDescription { get; }    // read-only
    }

    public interface IdentifierDelegate {
       string theIdentifier { get; }    // read-only    
    }

    public interface IdentifierCellTypeDelegate : CellTypeDelegate, IdentifierDelegate {}

    public interface DateDelegate {    
        DateTime theDate { get; }    // read-only     //DateTime Struct
    }

    public interface AnyToIntDelegate {
        int toInteger();
    }

    public interface WiFiSsidDelegate {
    
        string theSSID { get; }     // read-only

        string theBSSID { get; }    // read-only    

        string thePassword { get; } // read-only
    }

}