// using System;

namespace ie.delegates.WiFiScans
{
    public interface IeDelegate : IdentifierCellTypeDelegate, WiFiSsidDelegate {

        string theStrength { get; }    // read-only

        string theCapabilities { get; }    // read-only
    }
}